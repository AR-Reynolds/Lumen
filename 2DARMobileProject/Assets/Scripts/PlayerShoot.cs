using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera MainCam;
    float x = 1;
    float y = 0;
    float timer;
    bool timerOn;
    Rigidbody2D bulletRigidBody;
    SpriteRenderer spriteRenderer;

    public float ammo = 0;
    public float chargeTime = 1;
    public float cooldown = 0.05f;
    public float bulletSpeed = 10;
    public bool shootEnabled = true;
    bool mouseShoot = true;

    public GameObject bulletstorage;
    public GameObject automatic;
    public Transform firepoint;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletRigidBody = GetComponent<Rigidbody2D>();
        timer = 0;
    }

    public void computerShoot()
    {
        Quaternion automaticRotation = transform.rotation;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = transform.position - worldMousePos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        automaticRotation = Quaternion.Euler(0, 0, rot + 180);

        GameObject bullet = Instantiate(automatic, firepoint.position, automaticRotation);
        bullet.transform.parent = bulletstorage.transform;
        if(timer < 2)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * bulletSpeed * chargeTime;
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * bulletSpeed;
        }
        Destroy(bullet, 3);
        StartCoroutine(CanShootAutomatic());
    }
    public void mobileBulletShoot()
    {
        bool isPC = FindFirstObjectByType<PlayerMovement>().allowKeyControls;
        if(!isPC)
        {
            FixedJoystick joystick = FindFirstObjectByType<FirepointFollow>().joystick;
            Quaternion automaticRotation = transform.rotation;
            Vector3 worldMousePos = new Vector2(joystick.Horizontal, joystick.Vertical);
            Vector3 rotation = transform.position - worldMousePos;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            automaticRotation = Quaternion.Euler(0, 0, rot + 180);

            GameObject bullet = Instantiate(automatic, transform.position, automaticRotation);
            bullet.transform.parent = bulletstorage.transform;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * bulletSpeed;
            Destroy(bullet, 3);
            StartCoroutine(CanShootAutomatic());
        }
    }
    IEnumerator CanShootAutomatic()
    {
        shootEnabled = false;
        yield return new WaitForSeconds(cooldown);
        shootEnabled = true;
    }
    public void OnAttack(InputValue value)
    {
        bool isPC = FindFirstObjectByType<PlayerMovement>().allowKeyControls;
        if (!isPC)
        {
            mobileBulletShoot();
        }
        else
        {
            computerShoot();
        }
    }
    void Timer()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }

    void Update()
    {
        {

            if (mouseShoot && shootEnabled)
            {
                //shoot at mouse position
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 shootDir = mousePosition - transform.position;
                shootDir.z = 0;
                shootDir.Normalize();
                x = shootDir.x;
                y = shootDir.y;
            }
            else if (shootEnabled && !mouseShoot)
            {
                //shoot with the player's move direction
                float tempX = Input.GetAxisRaw("Horizontal");
                float tempY = Input.GetAxisRaw("Vertical");
                if (tempX != 0 || tempY != 0)
                {
                    x = tempX;
                    y = tempY;
                }
            }

        }
    }
}
