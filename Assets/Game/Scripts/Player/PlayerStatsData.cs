using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Scriptable Objects/PlayerStatsData")]
public class PlayerStatsData : ScriptableObject
{
    public float baseSpeed;
    public float baseMaxHealth;
    //public float basePickUpRange;
    public float damageResistance;
    //public float damageAmplifier;
    public float healthAmplifier;
    public float speedAmplifier;
    //public float pickUpRangeAmplifier;
    //weapon range
    // weapon duration
    // exp amplifier
}
