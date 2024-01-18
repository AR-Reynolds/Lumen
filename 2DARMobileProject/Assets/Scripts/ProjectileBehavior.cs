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

    void Start()
    {
        bulletActive = true;
        if (hasSpread)
        {
            float randomSpeed = Random.Range(0f, 5f);
            rb.velocity = transform.right * (bulletSpeed + randomSpeed);
        }
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("LightpointTrigger");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "THELIGHT")
        {
            FindClosestEnemy().gameObject.GetComponent<ObjectBehavior>().Light();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject, outOfBoundsTime);
        }
    }
}
