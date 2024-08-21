using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    /*
     movement and jumping scripts from Dave / GameDev on Youtube.
     YT link: https://www.youtube.com/watch?v=f473C43s8nE 
     */

    //variables to be used by the movement script
    [SerializeField] float moveSpeed;
    public Transform orientation;

    //check for grounding for drag
    public float height;
    public int groundTarget;
    public static bool grounded;
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

    //adding some more physics elements like gravity to make movment feel better
    public float gravity = -9.8f;

    //for footsteps
    public AudioSource footsteps;
    public AudioSource jump_landing;
    public static bool walking;
        
    // Start is called before the first frame update
    void Start()
    {   
        // grounded = true;
        // aquire rigid body and lock rotation
        rb = GetComponent<Rigidbody>();
        // rb.freezeRotation = true;
        walking = false;
    }

    //get axis for the player object
    private void KeyboardInput()
    {
        horizontalInput = GameManager.controls.Gameplay.Move.ReadValue<Vector2>().x;
        verticalInput = GameManager.controls.Gameplay.Move.ReadValue<Vector2>().y;

        //get raw inputs for movement OLD
        // horizontalInput = Input.GetAxisRaw("Horizontal");
        // verticalInput = Input.GetAxisRaw("Vertical");

        //check jump status, grounded, and key input before jumping
        if (GameManager.controls.Gameplay.Jump.WasPressedThisFrame() && jumpStatus && grounded)
        {
            // set status
            // jumpStatus = false;

            // invoke function
            Jump();
        }

        if ((horizontalInput != 0 || verticalInput != 0) && grounded)
        {
            if (!walking)
            {
                walking = true;
                footsteps.Play();
            }
        }
        else
        {
            if (walking)
            {
                walking = false;
                footsteps.Stop();
            }
        }
    }

    //apply force to player based on direction of mouse
    private void MovePlayer()
    {
        //always walk in the direction you're looking
        movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            //apply force. constant 10 is just to make things go a bit faster and compound with movespeed.
            rb.AddForce(movementDir.normalized * moveSpeed * 10f, ForceMode.Force);

        //apply air speed when airborne
        else if (!grounded)
            rb.AddForce(movementDir.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
    }
    
    //function to handle jumping
    void Jump()
    {
        grounded = false;

        //apply force for jump, using impulse because we want to do it only once.
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        grounded = false;
    }

    //called after jump to reset jumpStatus. This can be used for bunny hopping
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
        if (currVelocity.magnitude > moveSpeed)
        {
            //apply speed limitation
            Vector3 velocityLimiter = currVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(velocityLimiter.x, rb.velocity.y, velocityLimiter.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(Player_Singleton.player_instance.GetPlayerState()) { 
            case (int) Player_Singleton.state.normal:
                // invoke functions
                KeyboardInput();

                // Commenting this out because it's messing with the player movement to have it called in update rn
                // MovePlayer();

                SpeedLimiter();

                if (overlaps == 0) {
                    grounded = false;
                }

                // apply drag if grounded
                if (grounded)
                    rb.drag = groundDrag;
                else
                    rb.drag = 0;
                break;
            case (int) Player_Singleton.state.freeze:
                // Leaving this for effect rn

                // walking = false;     
                // footsteps.Stop();
                rb.velocity = Vector3.zero;
                break;
        }
    }

    void FixedUpdate() {
        MovePlayer();
    }

    // Temporary experiment to try and fix grounded logic issue
    // There's a weird case where when scenes transition, a trigger won't exit the player's collider. So I am making this variable
    // a static int so it can be reset to zero whenever the player's position is set by something other than normal means of movement...
    public static int overlaps = 0;

    void OnTriggerEnter(Collider other) {
        // Debug.Log("Trigger colliding with player object: " + other.gameObject);
        // Debug.Log("Game object layer: " + other.gameObject.layer);
        // Debug.Log("groundTarget: " + groundTarget);
        if (other.gameObject.layer == groundTarget) {
            overlaps++;
            if (!grounded) {
                grounded = true;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y);    // Reset y velocity since player is grounded
                jump_landing.Play();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        // Debug.Log("Trigger exit: " + other.gameObject);
        if (other.gameObject.layer == groundTarget) {
            Debug.Log("Trigger exiting player. Overlaps: " + overlaps);
            if (overlaps <= 1) {
                grounded = false;
            }
            overlaps--;
        }
    }
}