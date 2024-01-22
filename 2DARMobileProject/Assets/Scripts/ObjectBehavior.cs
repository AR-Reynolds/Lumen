using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public GameObject puzzleObject;
    public GameObject lampLight;
    public Color32 lampEnabled;
    public Color32 lampDisabled;
    public Color32 puzzleObjectEnabled;
    public Color32 puzzleObjectDisabled;
    public Color32 lampCheckpoint;
    public Color32 lampPuzzle;
    public bool active;
    public bool burntOut;
    public bool isCheckpoint;
    public bool isPuzzle;

    public string brokenText;
    public string unlightTextPC;
    public string unlightTextMobile;

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
            TextMeshProUGUI desktopDialogue = GameObject.Find("SignDialoguePC").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mobileDialogue = GameObject.Find("SignDialogueMobile").GetComponent<TextMeshProUGUI>();
            bool isPC = FindObjectOfType<PlayerMovement>().allowKeyControls;

            if (isPC)
            {
                desktopDialogue.text = gameObject.GetComponent<SignDialogue>().desktopText;
            }
            else
            {
                mobileDialogue.text = gameObject.GetComponent<SignDialogue>().mobileText;
            }

            if (isPuzzle)
            {
                if(!puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampPuzzle;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    puzzleObject.GetComponent<SpriteRenderer>().color = puzzleObjectDisabled;
                }
                else if (puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampPuzzle;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    puzzleObject.GetComponent<SpriteRenderer>().color = puzzleObjectEnabled;
                }
            }
            active = true;
        }
        else
        {
            TextMeshProUGUI desktopDialogue = GameObject.Find("SignDialoguePC").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mobileDialogue = GameObject.Find("SignDialogueMobile").GetComponent<TextMeshProUGUI>();
            bool isPC = FindObjectOfType<PlayerMovement>().allowKeyControls;

            if (isPC)
            {
                desktopDialogue.text = unlightTextPC;
            }
            else
            {
                mobileDialogue.text = unlightTextMobile;
            }

            if (isPuzzle)
            {
                if(!puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampDisabled;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    puzzleObject.GetComponent<SpriteRenderer>().color = puzzleObjectDisabled;
                }
                else if (puzzleObject.GetComponent<BoxCollider2D>().isTrigger)
                {
                    lampLight.GetComponent<SpriteRenderer>().color = lampDisabled;
                    puzzleObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    puzzleObject.GetComponent<SpriteRenderer>().color = puzzleObjectEnabled;
                }
            }
            active = false;
        }
    }

}
