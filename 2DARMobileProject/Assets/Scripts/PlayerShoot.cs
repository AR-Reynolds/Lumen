using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public bool active;
    public bool onCooldown;
    public int chargeLevel;
    public int ammoLeft;
    public int throwForce;
    public int basicDamage;
    public int cooldown;

    public GameObject lanternProjectile;
    public GameObject bulletstorage;
    public Transform firepoint;

    float x = 1;
    float y = 0;
    Rigidbody2D bulletRigidBody;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && active && !onCooldown)
        {
            bulletShoot();
        }
    }

    public void bulletShoot()
    {
        bool bulletSpeed = Mathf.Abs(bulletRigidBody.velocity.x) > Mathf.Epsilon;


        Quaternion basicRotation = transform.rotation;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = transform.position - worldMousePos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        basicRotation = Quaternion.Euler(0, 0, rot + 180);

        GameObject bullet = Instantiate(lanternProjectile, firepoint.position, basicRotation);
        bullet.transform.parent = bulletstorage.transform;
        if (bulletSpeed)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-x - chargeLevel, -y - chargeLevel) * throwForce;
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x + chargeLevel, y + chargeLevel) * throwForce;
        }
        Destroy(bullet, 3);
        ammoLeft -= 1;
        StartCoroutine(LanternCooldown());
    }
    IEnumerator LanternCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
