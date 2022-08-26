using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float rotationspeed;
    private Rigidbody playerRig;
    private Vector3 moveDirection;
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
       moveDirection = new Vector3(verticalInput, 0, horizontalInput);
    }

    //Update called 100times / second
    private void FixedUpdate()
    {
        playerRig.MovePosition(transform.position + vImput * Time.deltaTime * speed);

        //Rotating towards the direction of movement
        if (moveDirection != Vector3.zero)
        {
            playerRig.MoveRotation(Quaternion.RotateTowards(playerRig.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), rotationspeed * Time.fixedDeltaTime));
        }

    }
}
