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

}
