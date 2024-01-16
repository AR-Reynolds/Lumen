using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public bool bulletActive = true;
    public bool hasSpread = false;
    public float bulletSpeed = 20;
    [SerializeField] float fadeSpeed;
    [SerializeField] float outOfBoundsTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        bulletActive = true;
        if (hasSpread)
        {
            float randomSpeed = Random.Range(0f, 5f);
            rb.velocity = transform.right * (bulletSpeed + randomSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject, outOfBoundsTime);
        }
    }
}
