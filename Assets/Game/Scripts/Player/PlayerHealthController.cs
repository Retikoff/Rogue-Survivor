using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    /* 
        философский вопрос что лучше изменять SO или хранить переменную тут currentHealth и от нее плясать
    */
    
    private float currentHealth;
    public event Action<float> OnCurrentHealthChanged;
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            OnCurrentHealthChanged?.Invoke(currentHealth);
        }
    }

    void Start()
    {
        CurrentHealth = Managers.Player.PlayerStats.maxHealth;
    }

    public void TakeDamage(float value)
    {
        CurrentHealth -= value * (1 - Managers.Player.PlayerStats.damageResistance);
    }
}
