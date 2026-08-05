using UnityEngine;

public class BulletController : MonoBehaviour
{
    [HideInInspector] public float bulletLife = 1f;
    [HideInInspector] public float rotation = 0f;
    [HideInInspector] public float speed = 1f;
    [HideInInspector] public float damage = 1f; // мне не нравятся вот эти publicи

    private Vector2 spawnPoint;
    private float timer = 0f;

    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (timer > bulletLife)
        {
            Destroy(this.gameObject);
        }   
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer)
    {
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyManager>() != null)
        {
            collision.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}
