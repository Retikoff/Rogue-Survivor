using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRunData", menuName = "Scriptable Objects/PlayerRunData")]
public class PlayerRunData : ScriptableObject
{
    [Header("Run")]
    public float maxSpeed; // target speed player can reach
    public float acceleration; // Time time it takes for player to accelerate from 0 to maxSpeed
    [HideInInspector] public float accelAmount; // force applied to player (multiplied with speedDiff)
    public float decceleration; // Time it takes for player to accelerate from maxSpeed to 0
    [HideInInspector] public float deccelAmount; // force applied to the player (multiplied with speedDiff)
    [Space(10)]
    public bool doConserveMomentum;

    private void OnValidate()
    {
        accelAmount = (50 * acceleration) / maxSpeed;
        deccelAmount = (50 * decceleration) / maxSpeed;

        acceleration = Mathf.Clamp(acceleration, 0.01f, maxSpeed);
        decceleration = Mathf.Clamp(decceleration, 0.01f, maxSpeed);
    }
}
