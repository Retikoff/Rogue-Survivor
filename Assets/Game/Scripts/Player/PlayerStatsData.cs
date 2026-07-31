using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Scriptable Objects/PlayerStatsData")]
public class PlayerStatsData : ScriptableObject
{
    public float maxHealth;
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
    public float speed;
    public float acceleration;
    [HideInInspector] public float accelAmount;
    public float decceleration;
    [HideInInspector] public float deccelAmount;
    public bool doConserveMomentum;


    
    [Header("Weapons")]
    public int maxWeaponNum;

    private void OnValidate()
    {
        accelAmount = (50 * acceleration) / speed;
        deccelAmount = (50 * decceleration) / speed;

        acceleration = Mathf.Clamp(acceleration, 0.01f, speed);
        decceleration = Mathf.Clamp(decceleration, 0.01f, speed);
    }
}
