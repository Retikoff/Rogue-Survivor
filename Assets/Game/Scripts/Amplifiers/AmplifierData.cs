using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmplifierData", menuName = "Scriptable Objects/AmplifierData")]
public class AmplifierData : ScriptableObject
{
    public PlayerStats playerStat;
    public int maxLevel;
    public float baseValue;
    public List<float> levelUpgrades;
}
