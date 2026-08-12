using System;
using Unity.Profiling;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PlayerExperienceController : MonoBehaviour
{
    private int currentLvl;
    public event Action OnPlayerLevelUped;
    public int CurrentLvl
    {
        get => currentLvl;
        set
        {
            currentLvl = value;
            OnPlayerLevelUped?.Invoke();
        }
    }

    private float currentExp;
    public event Action<float> OnCurrentExpChanged;
    public float CurrentExp
    {
        get => currentExp;
        set
        {
            currentExp = value;
            OnCurrentExpChanged?.Invoke(currentExp);
        }
    }

    private float currentExpToProgress;
    public event Action<float> OnCurrentExpToProgressChanged;
    public float CurrentExpToProgress
    {
        get => currentExpToProgress;
        set
        {
            currentExpToProgress = value;
            OnCurrentExpToProgressChanged?.Invoke(currentExpToProgress);
        }
    }

    private void Start()
    {
        currentLvl = 1;
        CurrentExp = 0f;
        CurrentExpToProgress = 5f;
    }

    public void GetExp(float value)
    {
        CurrentExp += value * PlayerManager.PlayerStats.expAmplifier;

        while (CurrentExp >= CurrentExpToProgress)
        {
            CurrentExp -= CurrentExpToProgress;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        CurrentLvl += 1;

        ChangeExpToProgress();

        Debug.Log("Current lvl: " + currentLvl);
        Debug.Log("Current exp: " + currentExp);
        Debug.Log("New exp goal: " + currentExpToProgress);
    }

    private void ChangeExpToProgress()
    {
        if (currentLvl <= 20)
        {
            CurrentExpToProgress += 10f;
        }
        else if (currentLvl > 20 && currentLvl <= 40)
        {
            CurrentExpToProgress += 13f;
        }
        else if (currentLvl > 40)
        {
            CurrentExpToProgress += 16f;
        }
    }
}
