using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HandMovementR : MonoBehaviour
{
    public static bool itemEquip;
    public static Rigidbody handR;
    private CharacterJoint pocketJoint;
    private bool idle;

    void Start()
    {
        handR = GetComponent<Rigidbody>();
        idle = true;
        handR.sleepThreshold = 0;
        itemEquip = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) & !itemEquip)
        {
            itemEquip = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            itemEquip = false;
        }
    }

    void FixedUpdate()
    {
        if (handR.GetComponent<CharacterJoint>() == null)
        {
            idle = false;
        }
        else
        {
            idle = true;
        }

        if (!itemEquip & !idle)
        {
            handR.useGravity = false;
            pocketJoint = SoftJointManager.pocketString(handR.gameObject, "right");
        }
        else if (itemEquip & idle)
        {
            Destroy(pocketJoint);
        }

        if (idle && pocketJoint.connectedAnchor != Vector3.zero)
        {
            pocketJoint.connectedAnchor = Vector3.Lerp(pocketJoint.connectedAnchor, new Vector3(0, 0.2f, 0), 0.1f);
        }
        else
        {
            handR.useGravity = true;
        }
    }

}