using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Ray cameraSpotRay;
    RaycastHit cameraSpotRayHit;
    private Vector3 cameraSpot;
    private bool cameraOnPlayer;
    private Vector3 cameraPos;
    public float smoothSpeed;
    public RaycastHit lookHit;

    void Start()
    {
        smoothSpeed = 1.0f;
        cameraOnPlayer = true;
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
        if (Vector3.Distance(cameraSpot, Movement.playerRig.position) > 3.0f)
        {
            cameraOnPlayer = false;
            cameraPos = new Vector3(Movement.playerRig.position.x, transform.position.y, Movement.playerRig.position.z);
        }
       
    }

    private void FixedUpdate()
    {
        if (HeadMovement.leftClick)
        {
            Physics.Raycast(HeadMovement.headPos, HeadMovement.targetDirection, out lookHit);
            transform.position = Vector3.Lerp(transform.position, lookHit.point, smoothSpeed * Time.fixedDeltaTime * 1.5f);
        }
        else if (!cameraOnPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos, smoothSpeed * Time.fixedDeltaTime);
        }

        if (cameraSpotRayHit.transform?.tag == "Player" & !cameraOnPlayer)
        {
            cameraOnPlayer = true;
        }
    }
}
