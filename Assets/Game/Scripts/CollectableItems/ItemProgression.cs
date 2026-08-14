using System.Collections.Generic;
using GameUtils;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProgression", menuName = "Scriptable Objects/ItemProgression")]
public class ItemProgression : ScriptableObject
{
    // мне не нравиться что тут string, есть огромный шанс опечататься в инспекторе
    public Array2D<string> levels = new Array2D<string>(9, 3);
    /* 
    
    [
        ["damage:10", "projectileSpeed:5", "projectileCount:5"],
        ["projectileSize:5", "", ""],
        ["", "", ""],
        ...
        ["", "", ""]
    ]
    
    */
}
