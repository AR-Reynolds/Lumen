using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Defaults")]
    public int startHealth = 5;
    public float walkSpeed = 10f;
    public float crouchSpeed = 10f;
    public float jumpSpeed = 8f;
    public float climbSpeed = 5f;
    public float gravity = 2.5f;

    [Header("Player States")]
    public bool isAlive = true;
    public bool isCrouching = false;
    public Vector2 standSize;
    public Vector2 crouchSize;

    [Header("Miscellaneous")]
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    BoxCollider2D playerBodyCollider;
    CapsuleCollider2D playerJumpCollider;
    SpriteRenderer spriteRenderer;
    Canvas pauseCanvas;
    float startGravity;
    [Tooltip("Joystick for mobile")]
    [SerializeField] public FloatingJoystick joystick;
    [Tooltip("Changes to keyboard controls (and disables mobile UI) if set to true.")]
    [SerializeField] public bool allowKeyControls = true;

    void Start()
    {
        PlayerPrefs.SetInt("Health", startHealth);
        playerJumpCollider = GetComponent<CapsuleCollider2D>();
        playerBodyCollider = GetComponent<BoxCollider2D>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pauseCanvas = GameObject.Find("PauseUI").GetComponent<Canvas>();
        startGravity = gravity;
        playerRigidBody.gravityScale = startGravity;
        standSize = playerBodyCollider.size;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        if(!allowKeyControls)
        {
            Vector2 move = new Vector2(0, 0);
            move.x = joystick.Horizontal;
            move.y = joystick.Vertical;
            if(!isCrouching)
            {
                playerRigidBody.velocity = new Vector3(move.x * walkSpeed, playerRigidBody.velocity.y, 0);
            }
            else
            {
                playerRigidBody.velocity = new Vector3(move.x * crouchSpeed, playerRigidBody.velocity.y, 0);
            }
        }
        PlayerWalk();
        Die();
    }
    // moving for PC
    void OnMove(InputValue value)
    {
        if (!isAlive || !allowKeyControls || pauseCanvas.enabled)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }
    // jumping for PC
    void OnJump(InputValue value)
    {
        if (!isAlive || !allowKeyControls || pauseCanvas.enabled) { return; }
        if (!playerJumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || playerJumpCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    // crouching for PC
    void OnCrouch(InputValue value)
    {
        if (!isAlive || !allowKeyControls || pauseCanvas.enabled)
        {
            return;
        }
        bool uncrouch = FindObjectOfType<CrouchDetector>().bumpDetected;

        if (value.isPressed)
        {
            if (!isCrouching)
            {
                isCrouching = true;
                playerBodyCollider.size = crouchSize;
            }
            else
            {
                if (!uncrouch)
                {
                    isCrouching = false;
                    playerBodyCollider.size = standSize;
                }
            }
        }
    }
    // also walking for PC
    void PlayerWalk()
    {
        if (!isAlive || !allowKeyControls || pauseCanvas.enabled)
        {
            return;
        }
        if (!isCrouching)
        {
            Vector2 playerWalkVelocity = new Vector2(moveInput.x * walkSpeed, playerRigidBody.velocity.y);
            playerRigidBody.velocity = playerWalkVelocity;
        }
        else
        {
            Vector2 playerCrouchVelocity = new Vector2(moveInput.x * crouchSpeed, playerRigidBody.velocity.y);
            playerRigidBody.velocity = playerCrouchVelocity;
        }
    }
    public void MobileJump()
    {
        if (!isAlive || allowKeyControls || pauseCanvas.enabled)
        {
            return;
        }
        if (!playerJumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || playerJumpCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }
        playerRigidBody.velocity += new Vector2(0f, jumpSpeed);
    }
    public void MobileCrouch()
    {
        if (!isAlive || allowKeyControls || pauseCanvas.enabled)
        {
            return;
        }

        bool uncrouch = FindObjectOfType<CrouchDetector>().bumpDetected;
        if (!isCrouching)
        {
            isCrouching = true;
            playerBodyCollider.size = crouchSize;
        }
        else
        {
            if (!uncrouch)
            {
                isCrouching = false;
                playerBodyCollider.size = standSize;
            }   
        }
    }
    void Die()
    {
        if (PlayerPrefs.GetInt("Health") < 1)
        {
            isAlive = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DialogueTrigger")
        {
            TextMeshProUGUI desktopDialogue = GameObject.Find("SignDialoguePC").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mobileDialogue = GameObject.Find("SignDialogueMobile").GetComponent<TextMeshProUGUI>();
            
            if(allowKeyControls)
            {
                desktopDialogue.enabled = true;
                desktopDialogue.text = collision.gameObject.GetComponent<SignDialogue>().desktopText;
            }
            else
            {
                mobileDialogue.enabled = true;
                mobileDialogue.text = collision.gameObject.GetComponent<SignDialogue>().mobileText;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DialogueTrigger")
        {
            TextMeshProUGUI desktopDialogue = GameObject.Find("SignDialoguePC").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI mobileDialogue = GameObject.Find("SignDialogueMobile").GetComponent<TextMeshProUGUI>();

            desktopDialogue.enabled = false;
            mobileDialogue.enabled = false;
        }
    }
}