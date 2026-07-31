using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    /* 
        философский вопрос что лучше изменять SO или хранить переменную тут currentHealth и от нее плясать
    */
    [SerializeField] private float maxHealth = 100;
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
