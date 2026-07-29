using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Scriptable Objects/EnemyAttackData")]
public class EnemyAttackData : ScriptableObject
{
    public enum EnemyAttackType
    {
        Melee,
        Range
    }
    public EnemyAttackType type;
    public float damage;
    [ConditionalField("type", EnemyAttackType.Range)]
    public float firingRate; // ??
    //damageScale??
}
