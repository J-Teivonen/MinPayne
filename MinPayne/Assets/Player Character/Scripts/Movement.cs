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
   

    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody>();
        speed = 3;
        rotationspeed = 300;
    }

    // Update is called once per frame
    void Update()
    {
       horizontalInput = Input.GetAxis("Horizontal");
       verticalInput = Input.GetAxis("Vertical");
       vImput = new Vector3(verticalInput, playerRig.velocity.y, horizontalInput);
       if (horizontalInput + verticalInput != 0)
       {
            moveDirection = new Vector3(verticalInput, 0, horizontalInput);
       }
    
    }

    //Update called 100times / second
    private void FixedUpdate()
    {
        playerRig.MovePosition(transform.position + vImput * Time.deltaTime * speed);

        if (transform.forward.normalized != HeadMovement.facingDirection.normalized)
        {
            playerRig.MoveRotation(Quaternion.RotateTowards(playerRig.rotation, Quaternion.LookRotation(HeadMovement.facingDirection, Vector3.up), rotationspeed / 1.5f * Time.fixedDeltaTime));
        }

    }
}
