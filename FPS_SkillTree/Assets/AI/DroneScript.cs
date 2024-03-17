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

    //Rotation
    private Vector3 lookPoint;

    //Shoot
    public List<GameObject> socketList;
    public GameObject bullet;

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
        if (canScan == true )
        {
            scan();
            canScan = false;
        }
    }

    private void FixedUpdate()
    {
        //modify rayDistance in relation to the velocity in order to give anticipation movement
        rayDistance = Mathf.Clamp(rb.velocity.magnitude * rayDistanceRatio, rayDistanceMin, rayDistanceMax);
        detection();
        move();
        look();
        shoot();
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
        rb.AddForce(moveDirection.x * horizontalSpeed, moveDirection.y * verticalSpeed, moveDirection.z * horizontalSpeed, ForceMode.Force);
        Debug.DrawRay(this.transform.position,new Vector3 (moveDirection.x * horizontalSpeed, moveDirection.y * verticalSpeed, moveDirection.z * horizontalSpeed), Color.white);
    }

    public void scan()
    {
        scannerAnimator.SetTrigger("Scan");
    }

    public void look()
    {
        if(rb.velocity.magnitude > 1)
        {
            rb.transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
        }

    }

    public void shoot()
    {
        foreach(GameObject gameObject in socketList)
        {
            Instantiate(bullet, gameObject.transform.position , Quaternion.identity);
            
        }
    }

}
