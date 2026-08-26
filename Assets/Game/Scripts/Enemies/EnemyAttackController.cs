using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerManager>() != null)
        {
            collider.gameObject.SendMessage("TakeDamage", EnemyManager.EnemyStats.damage);
        }
    }
}
