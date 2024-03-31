using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    public float jumpForce = 5f;
    private Vector3 moveDir;
    private Rigidbody rb;
    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(-3, 2, -123);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true; // Make sure gravity is enabled
    rb.isKinematic = false; // Make sure the Rigidbody is not kinematic
    rb.interpolation = RigidbodyInterpolation.Interpolate; // Improve collision detection for moving objects
    rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Change if the Rigidbody moves fast
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        //CheckGround();

        // If player is on the ground and the jump button is pressed, add an upward force
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {

        moveDir = orientation.forward * -verticalInput + -orientation.right * horizontalInput;

        // If we're grounded and not moving, apply a small force to allow for movement
        if (moveDir.magnitude >= 0.01f)
        {
            rb.AddForce(moveDir.normalized * (moveSpeed * 10f), ForceMode.Force);
        }
        // If we're moving or in the air, apply regular force
        else
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }

    //private void MovePlayer()
    //{
    //    // Calculate the direction based on inputs
    //    moveDir = orientation.forward * -verticalInput + orientation.right * -horizontalInput;

    //    // Apply a consistent force based on the input
    //    rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    //}


    /* I don't want to use a Raycasthit for this, only other option would be to use a charactermovement */

    //private void CheckGround()
    //{
    //    // Perform a raycast down to check for ground
    //    RaycastHit hit;
    //    float distanceToGround = 1.1f; // Distance to ground should be slightly longer than half the height of your player
    //    isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround);

    //    // Optionally, you can draw the ray in the scene for debugging purposes
    //    Debug.DrawRay(transform.position, Vector3.down * distanceToGround, Color.red);
    //}

    void OnCollisionStay(Collision collision)
    {
        // Check if the GameObject is standing on the ground
        // isGrounded = collision.contacts[0].normal == Vector3.up;

        // Check if the GameObject is standing on the ground
        foreach (ContactPoint contact in collision.contacts)
        {
            // Check if the contact is roughly upwards
            if (Vector3.Angle(contact.normal, Vector3.up) < 45) // assuming 45 degrees as ground
            {
                isGrounded = true;
                break; // Exit the loop if a ground contact point is found
            }
        }
    }

    // This will be called when your GameObject stops colliding with another GameObject
    void OnCollisionExit(Collision collision)
    {
        // Mark as not grounded when not colliding
        isGrounded = false;
    }


}
