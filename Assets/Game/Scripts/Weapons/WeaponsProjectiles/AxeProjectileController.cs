using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AxeProjectileController : MonoBehaviour
{
    [HideInInspector] public WeaponData weaponData;
    private const float TORQUE_FORCE = 3f;

    private Rigidbody2D rb;
    private float timer = 0f;
    private int numGetThroughEnemies = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.linearVelocityY < -0.1f)
        {
            rb.AddTorque(TORQUE_FORCE, ForceMode2D.Force);
        }

        if (timer > weaponData.duration)
        {
            Destroy(this.gameObject);
        }

        timer += Time.deltaTime;
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

            collision.gameObject.SendMessage("TakeDamage", dealDamage);

            numGetThroughEnemies++;
            if (numGetThroughEnemies >= weaponData.pierce)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
