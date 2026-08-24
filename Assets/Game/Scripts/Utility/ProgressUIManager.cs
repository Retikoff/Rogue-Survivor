using UnityEngine;
using UnityEngine.UI;

public class ProgressUIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        Managers.ExperienceManager.OnCurrentExpChanged += SetValue;
        Managers.ExperienceManager.OnCurrentExpToProgressChanged += SetMaxValue;
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
