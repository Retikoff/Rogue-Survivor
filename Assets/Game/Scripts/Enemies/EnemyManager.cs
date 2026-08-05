using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyStatsData enemyStats;
    static public EnemyStatsData EnemyStats;

    private void Awake()
    {
        EnemyStats = enemyStats;
    }
}
