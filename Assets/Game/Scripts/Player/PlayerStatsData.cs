using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Scriptable Objects/PlayerStatsData")]
public class PlayerStatsData : ScriptableObject
{
    [Min(1f)]public float maxHealth;
    //public float basePickUpRange;
    public float damageResistance;
    //public float damageAmplifier;
    public float healthAmplifier;
    public float speedAmplifier;
    //public float pickUpRangeAmplifier;
    //weapon range
    // weapon duration
    // exp amplifier

    [Header("Run")]
    [Min(0f)] public float speed;
    public float acceleration;
    [HideInInspector] public float accelAmount;
    public float decceleration;
    [HideInInspector] public float deccelAmount;
    public bool doConserveMomentum;


    
    [Header("Weapons")]
    [Range(1, 6)] public int maxWeaponNum;

    
    [Header("Amplifiers")]
    [Range(1,6)] public int maxAmplifiersNum;

    private void OnValidate()
    {
        accelAmount = (50 * acceleration) / speed;
        deccelAmount = (50 * decceleration) / speed;

        acceleration = Mathf.Clamp(acceleration, 0.01f, speed);
        decceleration = Mathf.Clamp(decceleration, 0.01f, speed);
    }
}
