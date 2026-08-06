using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    private float currentHealth;

    private void Start()
    {
        currentHealth = EnemyManager.EnemyStats.maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
    }

    public void Death()
    {
        //animation?!
        
        if (UnityEngine.Random.Range(0, 100) < EnemyManager.EnemyStats.shardDropChance)
        {
            Instantiate(EnemyManager.EnemyStats.expShard, transform.position, transform.rotation);
        }

        Destroy(this.gameObject);
    }
}
