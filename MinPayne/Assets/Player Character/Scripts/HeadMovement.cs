using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeadMovement : MonoBehaviour
{
    private float resetTimer;
    public static bool leftClick;
    public static Vector3 headPos;
    public static Vector3 facingDirection;
    public static Vector3 targetPosition;
    public static Vector3 targetDirection;
    Ray cameraRay;
    RaycastHit cameraRayHit;
    private string[] hitList;

    void Start()
    {
        transform.Rotate(transform.forward);
        hitList = new string[] { "Furniture", "Floor", "Wall", "Player" };
    }

    void Update()
    {
        //Mouse follow activation
        if (Input.GetKey("mouse 0"))
        {
            leftClick = true;
            resetTimer = 0.0f;
            headPos = transform.position;
        }
    }

    private void FixedUpdate()
    {
        //Mouse placement ingame detection based on camera ray collision
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            if (hitList.Contains(cameraRayHit.transform.tag))
            {
                targetPosition = cameraRayHit.point;
                //Debug.Log(cameraRayHit.transform.tag);
                targetDirection = targetPosition - transform.position;
            }
        }

        //Head rotation
        if (Movement.moveDirection != Vector3.zero & !leftClick)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Lerp(Quaternion.LookRotation(Movement.moveDirection, Vector3.up), Movement.playerRig.rotation, 0.5f), Movement.rotationspeed * Time.fixedDeltaTime);
        }
        else if (leftClick)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Slerp(Quaternion.LookRotation(targetDirection, Vector3.up), Movement.playerRig.rotation, 0.5f), Movement.rotationspeed * Time.fixedDeltaTime);
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
