using System.Collections.Generic;
using GameUtils;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProgression", menuName = "Scriptable Objects/ItemProgression")]
public class ItemProgression : ScriptableObject
{
    public Array2D<string> levels = new Array2D<string>(9, 4);
}
