using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public GameObject lampLight;
    public Color32 lampEnabled;
    public Color32 lampDisabled;
    public Color32 lampCheckpoint;
    public Color32 lampPuzzle;
    public bool active;
    public bool burntOut;
    public bool isCheckpoint;
    public bool isPuzzle;

    void Start()
    {
        burntOut = false;
    }
    private void LateUpdate()
    {
        if (active)
        {
            if (!isCheckpoint && !isPuzzle)
            {
                lampLight.GetComponent<SpriteRenderer>().color = lampEnabled;
            }
            else if (isCheckpoint)
            {
                lampLight.GetComponent<SpriteRenderer>().color = lampCheckpoint;
            }
            else if (isPuzzle)
            {
                lampLight.GetComponent<SpriteRenderer>().color = lampPuzzle;
            }

        }
    }

    public void Light()
    {
        active = true;
    }

}
