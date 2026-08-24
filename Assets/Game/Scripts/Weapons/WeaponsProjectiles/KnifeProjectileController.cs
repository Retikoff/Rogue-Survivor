using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnifeProjectileController : MonoBehaviour
{
    [HideInInspector] public KnifesController weaponController;
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
        if (timer > weaponController.WeaponData.duration)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    private void MoveStraight()
    {
        rb.AddForce(weaponController.WeaponData.speed * transform.right, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyManager>() != null)
        {
            var body = collision.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                body.linearVelocity = (collision.transform.position - transform.position) * weaponController.WeaponData.knockback;
            }

            float dealDamage = weaponController.WeaponData.baseDamage;
            if (UnityEngine.Random.Range(0, 100) < weaponController.WeaponData.critChance)
            {
                dealDamage *= weaponController.WeaponData.critMulti;
            }
            collision.gameObject.SendMessage("TakeDamage", dealDamage); // damager multiplier?

            numGetThroughEnemies++;
            if (numGetThroughEnemies >= weaponController.WeaponData.pierce)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
