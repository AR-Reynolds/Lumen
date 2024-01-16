using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchDetector : MonoBehaviour
{
    BoxCollider2D bumpCollider;
    public bool bumpDetected = false;

    void Start()
    {
        bumpCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (bumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || bumpCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Debug.Log("Cannot uncrouch.");
            bumpDetected = true;
        }
        else
        {
            bumpDetected = false;
        }
    }
}
