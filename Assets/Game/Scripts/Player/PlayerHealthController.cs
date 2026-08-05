using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    /* 
        философский вопрос что лучше изменять SO или хранить переменную тут currentHealth и от нее плясать
    */
    private float currentHealth;
    [SerializeField] private HealthBarController healthBar;

    void Start()
    {
        currentHealth = PlayerManager.PlayerStats.maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;

        healthBar.SetHealth(currentHealth);
    }

    public void UpdateMaxHealth(float multiplier)
    {
        healthBar.SetMaxHealth(PlayerManager.PlayerStats.maxHealth * multiplier);
    }
}
