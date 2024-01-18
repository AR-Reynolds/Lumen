using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public GameObject puzzleObject;
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

        }
        else
        {
            lampLight.GetComponent<SpriteRenderer>().color = lampDisabled;
        }
    }

    public void Light()
    {
        if(!active)
        {
            if(isPuzzle)
            {
                if(!puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampPuzzle;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else if (puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampPuzzle;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
            active = true;
        }
        else
        {
            if(isPuzzle)
            {
                if(!puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampDisabled;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                else if (puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampDisabled;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
            active = false;
        }
    }

}
