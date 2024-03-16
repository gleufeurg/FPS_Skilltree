using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneScript : MonoBehaviour
{

    //Raycasts
    public float rayDistance;
    public LayerMask layerMask;
    public float subdivision;
    private RaycastHit rayHit;
    public float raycastNumber;
    private List<Vector3> rayList;
    private Vector3 directionUnit;
    private Vector3 direction;

    //Movement
    private Rigidbody rb;
    public float horizontalSpeed;
    public float verticalSpeed;
    private Vector3 moveDirection;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        rayList = new List<Vector3> ();
        direction = this.transform.right;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        detection();
        move();
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
        //moyenne des raycast verts

        if(rayList == null)
        {

        }
        else
        {
            moveDirection = new Vector3(rayList.Average(x => x.x), rayList.Average(x => x.y), rayList.Average(x => x.z)).normalized;
            //moveDirection = (rayList.Aggregate(Vector3.zero, (acc, v) => acc + v) / rayList.Count).normalized;
            //Debug.Log(moveDirection);
            rayList.Clear();
        }

    }

    public void rayDetection()
    {
        if (Physics.Raycast(new(this.transform.position, direction.normalized * rayDistance), out rayHit, rayDistance, layerMask))
        {
            directionUnit = (rayHit.normal);
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

}
