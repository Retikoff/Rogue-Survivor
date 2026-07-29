using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private EnemyAttackData Data;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", Data.damage);
        }
    }
}
