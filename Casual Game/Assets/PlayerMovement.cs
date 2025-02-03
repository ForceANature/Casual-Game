using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //public float moveSpeed = 6;
    public float pushForce = 3f;
    //public float pushForceGate = 1f;
    public float pushAngle = 45f;
    Vector3 verticalForceDirection;
    Vector3 horizontalForceDirection;
    //Movement Crap /\
    //              ||
    //Mouse Crap    \/
    public Vector2 turn;
    public float sensitivity = 3f;
    //Mouse Crap    /\
    //              ||
    //Body Parts    \/  (For Head Rotation and More) - WIP
    public GameObject head;
    public Rigidbody rb;
    public float groundCheckDistance = 0;
    void Start()//What exiqutes before the first frame
    {
        rb = GetComponent<Rigidbody>();// Get this shit to work or lock in where it need in a code or something, idk..

        Cursor.lockState = CursorLockMode.Locked;// Mouse locked and invisible
    }

    void Update()// Does shit every "frame"
    {
        //------------------------------------------------------------------------------ For Mouse input control
        turn.x += Input.GetAxis("Mouse X") * sensitivity;// Change of X mouse input
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;// Change of Y mouse input
        
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);//Horizontal for the body

        head.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);//Vertical for the Head

        //------------------------------------------------------------------------------ X & Y movement control
        //transform.Translate(moveSpeed * Time.deltaTime * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));// Movement based on the axis and set speed, deltaTime for frame and smoothness bullshit


        //------------------------------------------------------------------------------ Jumping movement
        //if (Input.GetButtonDown("Jump") && Mathf.Approximately(0, rb.velocity.y)) rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);// Gives Velocity to the body "connected"

        //------------------------------------------------------------------------------ "Hopping" movement

        //if (Input.GetButtonDown("Jump")) rb.AddForce(Vector3.up * pushForce, ForceMode.Impulse);// WTFuck is going on here, cannot figure it out // Pushes a mf up by pushForce

        if (IsGrounded())
        {
            //Trying to set velocity to 0 when grounded, to stop inertia //Take 1
            rb.velocity = new Vector2(0, 0);


            /*
            //rb.AddForce(Vector3.up, ForceMode.Impulse);// Trying to push it up, end up pushing non-stop

            forceDirection = Input.GetAxisRaw("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right;// Point to directions: (Forward/Backwards, Right/Left)-From the point of the camera view, and xUPx(WIP - didn't work)
            //Fuck, it can only jump forward and right, no back or left jump yet
            rb.AddForce(Vector3.up * pushForce, ForceMode.Impulse);// Pushes a mf UP(WIP)
            //This is fucking terrible
            rb.AddForce(forceDirection * pushForce, ForceMode.Impulse);// Pushes mf forward and to sides by pushForce
            */ // This was a fair try to make it work as intended -_-

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                verticalForceDirection = Vector3.up;
                horizontalForceDirection = Input.GetAxisRaw("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right;// Points to directions: (Forward/Backwards, Right/Left)-From the point of the camera view
                rb.AddForce((verticalForceDirection * pushAngle/* Pushing up more or less to create an angle */ + horizontalForceDirection) * pushForce, ForceMode.Impulse);// Pushes a mf forward and to sides, and UP by pushForce
            }
        }
        //This is fucking terrible

        //DEAL WITH INERTIA!!!!


        //------------------------------------------------------------------------------

    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance);
    }





}
