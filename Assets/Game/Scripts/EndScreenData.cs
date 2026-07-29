using UnityEngine;

/* 
    не уверен что это должно быть SO
*/

[CreateAssetMenu(fileName = "EndScreenData", menuName = "Scriptable Objects/EndScreenData")]
public class EndScreenData : ScriptableObject
{
    public Heroes hero;
    public Time timeSurvived;
    public Weapons[] weapons;
    public int[] weaponsLevels;
    public int goldCollected; // ??
}