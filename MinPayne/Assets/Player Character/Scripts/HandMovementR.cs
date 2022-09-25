using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HandMovementR : MonoBehaviour
{
    public static bool itemEquip;
    public static int item;
    public static string[] itemList;
    public static Rigidbody handR;
    private KeyCode[] numberKeys;
    private CharacterJoint pocketString;
    private bool idle;

    void Start()
    {
        handR = GetComponent<Rigidbody>();
        idle = true;
        handR.sleepThreshold = 0;
        itemEquip = false;
        item = 0;
        numberKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2 };
        itemList = new string[] { "camera", "gun" };
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

        if (!Input.anyKeyDown)
        {
            return;
        }
        for (int i = 0; i < numberKeys.Length; i++)
        {
            if (Input.GetKeyDown(numberKeys[i]))
            {
                item = i;
            }
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
            pocketString = handR.transform.AddComponent<CharacterJoint>();
            pocketString.anchor = new Vector3(0, -0.5f, 0);
            pocketString.axis = Vector3.forward;
            pocketString.highTwistLimit = SoftJointManager.jointFetch(pocketString.highTwistLimit, Vector3.zero);
            pocketString.lowTwistLimit = SoftJointManager.jointFetch(pocketString.lowTwistLimit, Vector3.zero);
            handR.GetComponent<CharacterJoint>().connectedBody = PocketFinder.pocket("right");
            Vector3 handStart = pocketString.connectedAnchor;
            pocketString.autoConfigureConnectedAnchor = false;
            pocketString.connectedAnchor = handStart;
        }
        else if (itemEquip & idle)
        {
            Destroy(pocketString);
        }

        if (idle && pocketString.connectedAnchor != Vector3.zero)
        {
            pocketString.connectedAnchor = Vector3.Lerp(pocketString.connectedAnchor, new Vector3(-0.3f, 0.1f, 0), 0.1f);
        }
        else
        {
            handR.useGravity = true;
        }
    }

}