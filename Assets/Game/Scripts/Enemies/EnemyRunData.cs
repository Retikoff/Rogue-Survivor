using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRunData", menuName = "Scriptable Objects/EnemyRunData")]
public class EnemyRunData : ScriptableObject
{
    public float maxSpeed;
    public float acceleration;
    [HideInInspector] public float accelAmount;
    public float decceleration;
    [HideInInspector] public float deccelAmount;
    public bool doConserveMomentum;

    private void OnValidate()
    {
        accelAmount = (50 * acceleration) / maxSpeed;
        deccelAmount = (50 * decceleration) / maxSpeed;

        acceleration = Mathf.Clamp(acceleration, 0.01f, maxSpeed);    
        decceleration = Mathf.Clamp(decceleration, 0.01f, maxSpeed);    
    }
}
