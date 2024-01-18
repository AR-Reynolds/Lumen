using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepointFollow : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spriteRenderer;

    public bool flipped = false;
    public float xOffset = 0;
    public float yOffset = 0;
    [SerializeField] public FixedJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        bool isPC = FindObjectOfType<PlayerMovement>().allowKeyControls;
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        if(!isPC)
        {
            Vector2 mouseOnScreen = (Vector2)joystick.transform.position;
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            Vector3 difference = joystick.transform.position - player.transform.position;
            difference.Normalize();
            float rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;

            if (rotZ > -95 && rotZ < 95 && !flipped)
            {
                flipped = true;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, 0);
            }
            else
            {
                if (rotZ > 95 && rotZ < 180)
                {
                    flipped = false;
                    transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 0);
                    return;
                }
            }
        }
        else
        {
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            Vector3 difference = Camera.main.ScreenToWorldPoint(UnityEngine.InputSystem.Mouse.current.position.ReadValue()) - player.transform.position;
            difference.Normalize();
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (rotZ > -95 && rotZ < 95 && !flipped)
            {
                flipped = true;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, 0);
            }
            else
            {
                if (rotZ > 95 && rotZ < 180)
                {
                    flipped = false;
                    transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 0);
                    return;
                }
            }
        }

    }
    void LateUpdate()
    {
        Vector3 coolPos = player.transform.position + new Vector3(xOffset, yOffset, 0);

        transform.position = Vector3.MoveTowards(transform.position, coolPos, 1);
    }
}
