using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            collider.gameObject.SendMessage("TakeDamage", EnemyManager.EnemyStats.damage);
        }
    }
}
