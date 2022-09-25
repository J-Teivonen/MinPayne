using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HandMovementL : MonoBehaviour
{
    public static Rigidbody handL;
    private CharacterJoint pocketString;
    private bool idle;

    void Start()
    {
        handL = GetComponent<Rigidbody>();
        handL.sleepThreshold = 0;
    }

    void FixedUpdate()
    {
        if (handL.GetComponent<CharacterJoint>() == null)
        {
            idle = false;
        }
        else
        {
            idle = true;
        }

        if (!HandMovementR.itemEquip & !idle)
        {
            handL.useGravity = false;
            pocketString = handL.transform.AddComponent<CharacterJoint>();
            pocketString.anchor = new Vector3(0, -0.5f, 0);
            pocketString.axis = Vector3.forward;
            pocketString.highTwistLimit = SoftJointManager.jointFetch(pocketString.highTwistLimit, Vector3.zero);
            pocketString.lowTwistLimit = SoftJointManager.jointFetch(pocketString.lowTwistLimit, Vector3.zero);
            handL.GetComponent<CharacterJoint>().connectedBody = PocketFinder.pocket("left");
            Vector3 handStart = pocketString.connectedAnchor;
            pocketString.autoConfigureConnectedAnchor = false;
            pocketString.connectedAnchor = handStart;
        }
        else if (HandMovementR.itemEquip & idle)
        {
            Destroy(pocketString);
        }

        if (idle && pocketString.connectedAnchor != Vector3.zero)
        {
            pocketString.connectedAnchor = Vector3.Lerp(pocketString.connectedAnchor, new Vector3(-0.3f, 0.1f, 0), 0.1f);
        }
        else
        {
            handL.useGravity = true;
        }
    }

}