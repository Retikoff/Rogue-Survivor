using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AchievementsData", menuName = "Scriptable Objects/AchievementsData")]
public class AchievementsData : ScriptableObject
{
    public enum TypeOfAchievement
    {
        Hero,
        Weapon
    }

    public TypeOfAchievement type;
    [ConditionalField("type", TypeOfAchievement.Hero)]
    public Heroes hero;
    [ConditionalField("type", TypeOfAchievement.Hero)]
    public float goalTime;

    [ConditionalField("type", TypeOfAchievement.Weapon)]
    public Weapons weapon;
    [ConditionalField("type", TypeOfAchievement.Weapon)]
    public int goalLevel;
    
}
