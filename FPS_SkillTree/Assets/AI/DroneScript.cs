using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneScript : MonoBehaviour
{
    //Scanner
    public GameObject scanner;
    private Animator scannerAnimator;
    private bool canScan;

    //Raycasts
    public float rayDistanceRatio;
    public float rayDistanceMin;
    public float rayDistanceMax;
    private float rayDistance;
    public LayerMask layerMask;
    public float subdivision;
    private RaycastHit rayHit;
    public float raycastNumber;
    private List<Vector3> rayList;
    private List<float> distanceList;
    private Vector3 directionUnit;
    private Vector3 direction;

    //Movement
    private Rigidbody rb;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float speedRatio;
    private float speedMultiplicator;
    private Vector3 moveDirection;
    public float acceptanceRadius;

    //Rotation
    private Vector3 lookPoint;
    public float rotationSpeed;
    public GameObject rotatingComponent;

    //Shoot
    public List<GameObject> socketList;
    public GameObject bullet;
    private bool canShoot;
    public float rafaleRateMin;
    public float rafaleRateMax;
    public float fireRateMin;
    public float fireRateMax;
    public int fireCount;
    private int currentFireCount;


    //chase
    private Vector3 chaseDirection;
    private GameObject playerRef;
    public float chaseSpeed;
    private bool canChase;
    public Collider rangeChase;
    private Vector3 lastPosition;
    private bool detected;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        rayList = new List<Vector3>();
        distanceList = new List<float>();
        direction = this.transform.right;
        scannerAnimator = scanner.GetComponent<Animator>();
        //socketList = new List<GameObject>();

    }

    // Start is called before the first frame update
    void Start()
    {
        canScan = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (canScan == true )
        {
            scan();
            canScan = false;
        }*/
    }

    private void FixedUpdate()
    {
        //modify rayDistance in relation to the velocity in order to give anticipation movement
        rayDistance = Mathf.Clamp(rb.velocity.magnitude * rayDistanceRatio, rayDistanceMin, rayDistanceMax);
        detection();
        move();
        look();

        if (canChase == true)
        {
            chase();
            
        }

    }

    public void detection()
    {
        Ray ray = new Ray();
        ray.origin = this.transform.position;
        float inverseResolution = subdivision;
        int steps = Mathf.FloorToInt(raycastNumber / inverseResolution);
        Quaternion yRotation = Quaternion.Euler(Vector3.up * inverseResolution);


        for (int x = 0; x < steps / 2; x++)
        {
            Quaternion zRotation = Quaternion.Euler(Vector3.forward * inverseResolution);
            direction = zRotation * direction;

            for (int y = 0; y < steps; y++)
            {
                Quaternion xRotation = Quaternion.Euler(Vector3.right * inverseResolution);

                direction = xRotation * direction;
                rayDetection();

                
            }
        }
        //moyenne des raycast noirs

        if(rayList == null)
        {

        }
        else
        {
            moveDirection = new Vector3(rayList.Average(x => x.x), rayList.Average(x => x.y), rayList.Average(x => x.z)).normalized;
            /*speedMultiplicator = distanceList.Average();
            speedMultiplicator = (speedMultiplicator);
            horizontalSpeed /= speedMultiplicator;
            verticalSpeed /= speedMultiplicator;
            distanceList.Clear();*/
            rayList.Clear();
        }

    }

    public void rayDetection()
    {
        if (Physics.Raycast(new(this.transform.position, direction.normalized * rayDistance), out rayHit, rayDistance, layerMask))
        {
            directionUnit = (rayHit.normal);
            //distanceList.Add(Mathf.Clamp(rayHit.distance, -1, 1));
            rayList.Add(new Vector3(directionUnit.x, directionUnit.y, directionUnit.z));
            Debug.DrawRay(this.transform.position,direction.normalized * rayDistance, Color.green);
            Debug.DrawRay(rayHit.point, rayHit.normal * 2, Color.black);
        }
        else
        {
            Debug.DrawRay(this.transform.position, direction.normalized * rayDistance, Color.red);
        }
    }

    public void move()
    {
        //need to add increase speed in relation to the distance to the closest hit
        if(canChase == true)
        {
            
            rb.AddForce(moveDirection.x * horizontalSpeed + chaseDirection.x * chaseSpeed, moveDirection.y * verticalSpeed + chaseDirection.y * chaseSpeed, moveDirection.z * horizontalSpeed + chaseDirection.z * chaseSpeed, ForceMode.Force);
            Debug.DrawRay(this.transform.position, new Vector3(moveDirection.x * horizontalSpeed + chaseDirection.x * chaseSpeed, moveDirection.y * verticalSpeed + chaseDirection.y * chaseSpeed, moveDirection.z * horizontalSpeed + chaseDirection.z * chaseSpeed), Color.magenta);
        }
        else
        {
            rb.AddForce(moveDirection.x * horizontalSpeed, moveDirection.y * verticalSpeed, moveDirection.z * horizontalSpeed, ForceMode.Force);
            Debug.DrawRay(this.transform.position, new Vector3(moveDirection.x * horizontalSpeed, moveDirection.y * verticalSpeed, moveDirection.z * horizontalSpeed), Color.white);
        }
        
    }

    public void scan()
    {
        scannerAnimator.SetTrigger("Scan");
    }

    public void chase()
    {
        chaseDirection =  playerRef.transform.position - transform.position;

        //rb.AddForce(chaseDirection * chaseSpeed, ForceMode.Force);
    }
    public void look()
    {
        rotatingComponent.transform.LookAt(playerRef.transform, rotatingComponent.transform.up);
        {
            
           // rb.transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canChase = true;
            playerRef = other.gameObject;
            canShoot = true;
            StartCoroutine(rafaleCooldown());
            Debug.Log("Chase");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //canChase = false;
            lastPosition = playerRef.transform.position;
            canShoot = false;
            chaseDirection = lastPosition;
            Debug.Log("Chase");
        }

    }

    public void shoot()
    {
        var socket = socketList[Random.Range(0, socketList.Count)];
        Instantiate(bullet, socket.transform.position, socket.transform.rotation);
        if(currentFireCount < fireCount - 1)
        {
            currentFireCount ++;
            StartCoroutine(shootCooldown());
        }
        else
        {
            currentFireCount = 0;
            StartCoroutine(rafaleCooldown());
        }


    }
    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(Random.Range(fireRateMin, fireRateMax));
        shoot();
        
    }

    IEnumerator rafaleCooldown()
    {
        yield return new WaitForSeconds(Random.Range(rafaleRateMin, rafaleRateMax));
        if(canShoot == true)
        {
            shoot();
        }
        
    }

}
