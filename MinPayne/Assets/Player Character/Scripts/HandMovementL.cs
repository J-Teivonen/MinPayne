using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HandMovementL : MonoBehaviour
{
    public static Rigidbody handL;
    private CharacterJoint pocketJoint;
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
            pocketJoint = SoftJointManager.pocketString(handL.gameObject, "left");

        }
        else if (HandMovementR.itemEquip & idle)
        {
            Destroy(pocketJoint);
        }

        if (idle && pocketJoint.connectedAnchor != Vector3.zero)
        {
            pocketJoint.connectedAnchor = Vector3.Lerp(pocketJoint.connectedAnchor, new Vector3(0, 0.2f, 0), 0.1f);
        }
        else
        {
            handL.useGravity = true;
        }
    }

}