using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WhipProjectileController : MonoBehaviour
{
    [HideInInspector] public WeaponData weaponData;

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
            collision.SendMessage("TakeDamage", dealDamage);
        }
    }
}
