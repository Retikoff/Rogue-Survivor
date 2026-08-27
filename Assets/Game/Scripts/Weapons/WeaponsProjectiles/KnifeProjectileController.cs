using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnifeProjectileController : MonoBehaviour
{
    [HideInInspector] public WeaponData weaponData;
    private float timer = 0f;

    private Rigidbody2D rb;

    private int numGetThroughEnemies = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveStraight();
    }

    private void Update()
    {
        if (timer > weaponData.duration)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    private void MoveStraight()
    {
        rb.AddForce(weaponData.speed * transform.right, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyManager>() != null)
        {
            var body = collision.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                body.linearVelocity = (collision.transform.position - transform.position) * weaponData.knockback;
            }

            float dealDamage = weaponData.baseDamage;
            if (UnityEngine.Random.Range(0, 100) < weaponData.critChance)
            {
                dealDamage *= weaponData.critMulti;
            }
            collision.SendMessage("TakeDamage", dealDamage); // damager multiplier?

            numGetThroughEnemies++;
            if (numGetThroughEnemies >= weaponData.pierce)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
