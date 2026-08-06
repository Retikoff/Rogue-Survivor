using UnityEngine;
using UnityEngine.UI;

public class ProgressUIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        PlayerExperienceController expController = GameObject.Find("Player").GetComponent<PlayerExperienceController>();
        expController.OnCurrentExpChanged += SetValue;
        expController.OnCurrentExpToProgressChanged += SetMaxValue;
    }
    public void SetValue(float value)
    {
        slider.value = value;
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }
}
