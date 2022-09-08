using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeadMovement : MonoBehaviour
{
    public GameObject Head;
    private Vector3 mousePos;
    private float resetTimer;
    private bool leftClick;
    public static Vector3 facingDirection;
    private Vector3 targetPosition;
    Ray cameraRay;
    RaycastHit cameraRayHit;

    void Start()
    {
        transform.Rotate(transform.forward);
    }

    void Update()
    {
        //Mouse follow activation
        if (Input.GetKey("mouse 0"))
        {
            leftClick = true;
            resetTimer = 0.0f;
        }
        //Mouse placement ingame detection based on camera ray collision
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            if (cameraRayHit.transform.tag != "Player")
            {
                targetPosition = new Vector3(cameraRayHit.point.x, cameraRayHit.point.y, cameraRayHit.point.z);
            }

        }
    }
    private void FixedUpdate()
    {
        //Head rotation
        if (Movement.moveDirection != Vector3.zero & leftClick == false)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Lerp(Quaternion.LookRotation(Movement.moveDirection, Vector3.up), Movement.playerRig.rotation, 0.5f), Movement.rotationspeed * Time.fixedDeltaTime);
        }
        else if (leftClick)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Lerp(Quaternion.LookRotation(targetPosition, Vector3.up), Movement.playerRig.rotation, 0.5f), Movement.rotationspeed * Time.fixedDeltaTime);
            Movement.moveDirection = transform.forward;
        }

        facingDirection = new Vector3(transform.forward.x, 0, transform.forward.z);

        //Reset mouse following action after two seconds
        resetTimer += Time.fixedDeltaTime;
        if (resetTimer > 2.0f)
        {
           leftClick = false;
        }
    }
}
