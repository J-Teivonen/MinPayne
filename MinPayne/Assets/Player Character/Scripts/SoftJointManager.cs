using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftJointManager : MonoBehaviour
{
    public static SoftJointLimit jointFetch(SoftJointLimit limitMod, Vector3 values)
    {
        limitMod.limit = values.x;
        limitMod.bounciness = values.y;
        limitMod.contactDistance = values.z;
        return limitMod;
    }
    public static CharacterJoint pocketString(GameObject anchorObj, string side)
    {
        CharacterJoint newJoint;
        newJoint = anchorObj.AddComponent<CharacterJoint>();
        newJoint.anchor = new Vector3(0, -0.6f, 0);
        newJoint.axis = Vector3.zero;
        newJoint.highTwistLimit = jointFetch(newJoint.highTwistLimit, new Vector3(-60, 0, 0));
        newJoint.lowTwistLimit = jointFetch(newJoint.lowTwistLimit, new Vector3(-90, 0, 0));
        newJoint.swing1Limit = jointFetch(newJoint.swing1Limit, new Vector3(20, 0, 0));
        newJoint.swing2Limit = jointFetch(newJoint.swing2Limit, new Vector3(20, 0, 0));
        anchorObj.GetComponent<CharacterJoint>().connectedBody = PocketFinder.pocket(side);
        Vector3 handStart = newJoint.connectedAnchor;
        newJoint.autoConfigureConnectedAnchor = false;
        newJoint.connectedAnchor = handStart;
        return newJoint;
    }

}
