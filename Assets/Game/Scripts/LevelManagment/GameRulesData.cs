using UnityEngine;

[CreateAssetMenu(fileName = "GameRulesData", menuName = "Scriptable Objects/GameRulesData")]
public class GameRulesData : ScriptableObject
{
    public GameObject[] accessableWeapons;
    public GameObject[] accessableAmplifiers;
    public int maxWeaponsNum;
    public int maxAmplifiersNum;

}
