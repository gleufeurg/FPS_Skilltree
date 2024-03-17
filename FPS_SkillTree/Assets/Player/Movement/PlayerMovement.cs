using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Range(0, 200)] float moveSpeed = 15f;

    [Space(5f)]
    
    [Header("Ground")]
    [SerializeField] [Range(0, 1f)] float groundDetectionDistance = .2f;
    [SerializeField] [Range(0, 10f)] float groundDrag = 5f;
    [SerializeField] float playerheight;
    [SerializeField] LayerMask groundMask;
    bool grounded;

    [Space(5f)]

    [Header("Jump")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] [Range(0,1f)] float airMultiplier = .35f;
    [SerializeField] float jumpForce;
    [SerializeField] bool readyToJump;

    [Space(5f)]

    [Header("Refs")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform orientation;

    Vector3 moveDirection;
    float horizontalInput;
    float verticalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * .5f + groundDetectionDistance, groundMask);
        if (grounded)
        {
            ResetJump();
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        InputHandler();
        ClampSpeed();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        //Ground
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        else
        //Air
        rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
    }
    void ClampSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //Clamp speed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; ;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void ResetJump()
    {
        readyToJump = true;
    }

    void InputHandler()
    {
        //XZ Movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Jump
        if(Input.GetKey(jumpKey) && readyToJump)
        {
            readyToJump = false;
            Jump();
        }
    }
}
