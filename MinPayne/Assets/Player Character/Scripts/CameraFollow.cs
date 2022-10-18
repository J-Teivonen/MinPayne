using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Ray cameraSpotRay;
    RaycastHit cameraSpotRayHit;
    private Vector3 cameraSpot;
    private bool cameraOnPlayer;
    private Vector3 cameraPos;
    private RaycastHit lookHit;
    private Vector3 zoomLook;
    private LayerMask lookMask;
    public float smoothSpeed;


    void Start()
    {
        smoothSpeed = 1.0f;
        cameraOnPlayer = true;
        lookMask = LayerMask.GetMask("Player Model");
    }

    void Update()
    {
        cameraSpotRay = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(cameraSpotRay, out cameraSpotRayHit))
        {
            cameraSpot = cameraSpotRayHit.point;
        }
    }

    void LateUpdate()
    {
        //Camera follow activates if camera's central position is too far away from player model's position.
        if (Vector3.Distance(cameraSpot, Movement.playerRig.position) > 3.0f)
        {
            cameraOnPlayer = false;
            cameraPos = new Vector3(Movement.playerRig.position.x, transform.position.y, Movement.playerRig.position.z);
        }
    }

    private void FixedUpdate()
    {
        //Zoom function to look further from the chracter model. Blocked by walls.
        if (HeadMovement.leftClick)
        {
            Physics.Raycast(HeadMovement.headPos, HeadMovement.targetDirection, out lookHit, lookMask);
            if (lookHit.transform?.tag != "Player")
            {
                zoomLook = new Vector3(lookHit.point.x, transform.position.y, lookHit.point.z);
            }
            transform.position = Vector3.Lerp(transform.position, zoomLook, smoothSpeed * Time.fixedDeltaTime * 1.5f);
            return;
        }
        //Camera returns to player.
        else if (!cameraOnPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos, smoothSpeed * Time.fixedDeltaTime);
        }

        if (cameraSpotRayHit.transform?.tag == "Player")
        {
            cameraOnPlayer = true;
        }
    }
}
