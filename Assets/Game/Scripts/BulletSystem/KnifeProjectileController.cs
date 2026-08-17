using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnifeProjectileController : MonoBehaviour
{
    [HideInInspector] public KnifesController weaponController;
    private float timer = 0f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveStraight();
    }

    private void Update()
    {
        if (timer > weaponController.Data.duration)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    private void MoveStraight()
    {
        rb.AddForce(weaponController.Data.speed * transform.right, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyManager>() != null)
        {
            collision.gameObject.SendMessage("TakeDamage", weaponController.Data.baseDamage); // damager multiplier?
        }
    }
}
