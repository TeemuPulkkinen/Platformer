using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //walking speed
    public float walkSpeed;

    //jumping speed
    public float jumpForce;

    //coin playing sound
    public AudioSource coinSound;

    // camera distance in z
    public float cameraDistZ = 6;

    // Rigidbody component
    private Rigidbody rb;

    // Collider component
    private Collider col;

    //flag to keep track of key pressing
    bool pressedJump = false;

    //size of the player
    private Vector3 size;

    // y that represents that you fell
    private float minY = -1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Grab our components
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // get the player size
        size = col.bounds.size;

        // set the camera position
        CameraFollowPlayer();
    }

    /* Update is called once per frame, but when working with Rigidbodies it is better to use FixedUpdate, which runs
    on fixed intervals */
    void FixedUpdate()
    {
        WalkHandler();

        JumpHandler();

        CameraFollowPlayer();

        FallHandler();
    }

    // check if the player fell
    private void FallHandler()
    {
        if (transform.position.y <= minY)
        {
            // Game over!
            GameManager.instance.GameOver();
        }
    }

    void WalkHandler()
    {
        //input X (Horizontal)
        float hAxis = Input.GetAxis("Horizontal");

        //input z (vertical)
        float vAxis = Input.GetAxis("Vertical");

        //Movement vector
        Vector3 movement = new Vector3(hAxis * walkSpeed * Time.deltaTime, 0, vAxis * walkSpeed * Time.deltaTime);

        // Calculate the new position, which is equal to old position (transform.position) + movement
        Vector3 newPos = transform.position + movement;

        // Move player
        rb.MovePosition(newPos);

        // check that we are moving
        if (hAxis != 0 || vAxis != 0)
        {
            // Movement direction
            Vector3 direction = new Vector3(hAxis, 0, vAxis);

            // option 1 : modify the transform of the player
            //transform.forward = direction;

            // option 2: using our rigidbody
            rb.rotation = Quaternion.LookRotation(direction);
        }
    }
    void JumpHandler()
    {
        //input on the Jump axis
        float jAxis = Input.GetAxis("Jump");

        //if key has been pressed
        if (jAxis > 0)
        {
            bool isGrounded = CheckGrounded();
            
            //make sure we are not already jumping
            if (!pressedJump && isGrounded) //exclamation mark means that if the parameter is true, if statement is false
            {
                pressedJump = true;

                //jumping vector
                Vector3 jumpVector = new Vector3(0, jAxis * jumpForce, 0);

                //apply force
                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }

            
        }
        else
        {
            //set the flag to false
            pressedJump = false;
        }
    }
    //metodi joka tarkistaa onko pelaaja maassa vai ei
    private bool CheckGrounded()
    {
        // location of all 4 corners of the player cube
        Vector3 corner1 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner3 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);

        // check if we are grounded
        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.02f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.02f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.02f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.02f);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // Increase the player score
            GameManager.instance.increaseScore(1);

            // Play coin sound
            coinSound.Play();

            // Destroy coin
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            // Game over!
            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("Goal"))
        {
            // send player to the next level
            GameManager.instance.increaseLevel();
        }
    }
    private void CameraFollowPlayer()
    {
        // grab the camera position
        Vector3 cameraPosition = Camera.main.transform.position;

        // modify its position according to cameraDistZ
        cameraPosition.z = transform.position.z - cameraDistZ;

        // set the camera position
        Camera.main.transform.position = cameraPosition;
    }
}
