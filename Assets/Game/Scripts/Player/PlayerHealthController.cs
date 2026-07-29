using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private PlayerStatsData Data;
    private float currentHealth;
    [SerializeField] private HealthBarController healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        //currentHealth = Data.baseMaxHealth
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;

        healthBar.SetHealth(currentHealth);
    }

    public void UpdateMaxHealth(float multiplier)
    {
        healthBar.SetMaxHealth(maxHealth * multiplier);
    }
}
