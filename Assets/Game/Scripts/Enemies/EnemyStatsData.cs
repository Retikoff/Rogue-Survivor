using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsData", menuName = "Scriptable Objects/EnemyStatsData")]
public class EnemyStatsData : ScriptableObject
{
    public enum EnemyAttackType
    {
        Melee,
        Range
    }

    [Header("Movement")]
    public float maxSpeed;
    public float acceleration;
    [HideInInspector] public float accelAmount;
    public float decceleration;
    [HideInInspector] public float deccelAmount;
    public bool doConserveMomentum;

    [Header("Attack")]
    public EnemyAttackType type;
    public float damage;
    [ConditionalField("type", EnemyAttackType.Range)]
    public float firingRate; //??
    // damageScale??

    [Header("Health")]
    public float maxHealth;
    //public float healthScale;

    [Header("DeathShard")]
    public GameObject expShard = null;
    [Range(0,100)] public float shardDropChance;


    private void OnValidate()
    {
        #region movement
        accelAmount = (50 * acceleration) / maxSpeed;
        deccelAmount = (50 * decceleration) / maxSpeed;

        acceleration = Mathf.Clamp(acceleration, 0.01f, maxSpeed);
        decceleration = Mathf.Clamp(decceleration, 0.01f, maxSpeed);
        #endregion 

        #region expShardCheck

        // if (expShard.GetComponent<CollectableItemController>().Data.type != ItemType.ExpShard)
        // {
        //     expShard = null;
        // }

        #endregion
    }
}
