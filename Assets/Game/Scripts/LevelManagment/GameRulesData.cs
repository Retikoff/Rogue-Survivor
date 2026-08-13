using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameRulesData", menuName = "Scriptable Objects/GameRulesData")]
public class GameRulesData : ScriptableObject
{
    public List<CollectableItemController> accessableWeapons;
    public List<CollectableItemController> accessableAmplifiers;
    public int maxWeaponsNum;
    public int maxAmplifiersNum;

}
