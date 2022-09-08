using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static float speed;
    public static float rotationspeed;
    public static Rigidbody playerRig;
    public static Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 vImput;
   

    void Start()
    {
        playerRig = GetComponent<Rigidbody>();
        speed = 3;
        rotationspeed = 300;
    }

    void Update()
    {
        //Player input
       horizontalInput = Input.GetAxis("Horizontal");
       verticalInput = Input.GetAxis("Vertical");
       vImput = new Vector3(verticalInput, playerRig.velocity.y, horizontalInput);
       if (horizontalInput + verticalInput != 0)
       {
            moveDirection = new Vector3(verticalInput, 0, horizontalInput);
       }
    
    }

    private void FixedUpdate()
    {
        //Player movement
        playerRig.MovePosition(transform.position + vImput * Time.deltaTime * speed);

        //Rotate body towards head facing
        if (transform.forward.normalized != HeadMovement.facingDirection.normalized)
        {
            playerRig.MoveRotation(Quaternion.RotateTowards(playerRig.rotation, Quaternion.LookRotation(HeadMovement.facingDirection, Vector3.up), rotationspeed / 1.5f * Time.fixedDeltaTime));
        }

    }
}
