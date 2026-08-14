using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        PlayerHealthController healthController = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        healthController.OnCurrentHealthChanged += SetHealth;
        Managers.Player.PlayerStats.OnMaxHealthChanged += SetMaxHealth;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
