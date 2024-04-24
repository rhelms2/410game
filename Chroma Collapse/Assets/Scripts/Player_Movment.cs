using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movment : MonoBehaviour
{
    /*movement and jumping scripts from Dave / GameDev on Youtube.
     YT link: https://www.youtube.com/watch?v=f473C43s8nE */

    //set jump input
    public KeyCode jumpKey = KeyCode.Space;

    //variables to be used by the movement script
    public float moveSpeed;
    public Transform orientation;

    //check for grounding for drag
    public float height;
    public LayerMask groundTarget;
    bool grounded;
    public float groundDrag;

    //variables for player input
    float horizontalInput;
    float verticalInput;

    //jumping variables
    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    bool jumpStatus = true;

    //physics elements
    Vector3 movementDir;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //aquire rigid body and lock rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    //get axis for the player object
    private void KeyboardInput()
    {
        //get raw inputs for movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //check jump status, grounded, and key input before jumping
        if(Input.GetKey(jumpKey) && jumpStatus && grounded)
        {
            //set status
            jumpStatus = false;

            //invoke function
            Jump();

            //uncomment this if you want to continuously jump with a cooldown, like holding nspace bar keeps jumping.
            Invoke(nameof(JumpReset), jumpCooldown);
        }
    }

    //apply force to player based on direction of mouse
    private void MovePlayer()
    {
        //always walk in the direction you're looking
        movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
            //apply force. constant 10 is just to make things go a bit faster and compound with movespeed.
            rb.AddForce(movementDir.normalized * moveSpeed * 10f, ForceMode.Force);
            
            //apply air speed when airborne
        else if(!grounded)
            rb.AddForce(movementDir.normalized * moveSpeed * 10f *airMult, ForceMode.Force);
    }

    //function to handle jumping
    void Jump()
    {
        //reset y velocity to make jump height more consistent.
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //apply force for jump, using impulse because we want to do it only once.
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //called after jump to reset jumpStatus
    void JumpReset()
    {
        jumpStatus = true;
    }
    //speed limiterto prevent high-velocity and feeling like you're on ice
    void SpeedLimiter()
    {
        //keep track of current velocity
        Vector3 currVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        //check if current velocity exceed move speed
        if(currVelocity.magnitude > moveSpeed)
        {
            //apply speed limitation
            Vector3 velocityLimiter = currVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(velocityLimiter.x, rb.velocity.y, velocityLimiter.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //perform raycast from player object with distance of half its height plus some extra (0.2f) to whatever ground is in this context.
        grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f, groundTarget);

        //invoke functions
        KeyboardInput();
        SpeedLimiter();

        //apply drag if grounded
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        //invoke function
        MovePlayer();
    }
}
