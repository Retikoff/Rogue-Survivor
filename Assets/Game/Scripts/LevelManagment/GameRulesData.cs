using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameRulesData", menuName = "Scriptable Objects/GameRulesData")]
public class GameRulesData : ScriptableObject
{
    public List<CollectableItemController> accessibleWeapons;
    public List<CollectableItemController> accessibleAmplifiers;
}
