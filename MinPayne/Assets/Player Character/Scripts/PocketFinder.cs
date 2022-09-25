using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketFinder : MonoBehaviour
{
    public static Rigidbody pocketR;
    public static Rigidbody pocketL;
    private Rigidbody pocketSide;

    void Start()
    {
        pocketSide = GetComponent<Rigidbody>();
        if (pocketSide.transform.localPosition.x > 0)
        {
            pocketR = pocketSide;
        }
        else
        {
            pocketL = pocketSide;
        }
    }

    public static Rigidbody pocket(string direction)
    {
        if (direction == "right")
        {
            return pocketR;
        }
        else
        {
            return pocketL;
        }
    }
}
