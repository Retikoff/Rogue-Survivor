using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItemData", menuName = "Scriptable Objects/CollectableItemData")]
public class CollectableItemData : ScriptableObject
{
    public ItemType type;

    [ConditionalField("type", ItemType.Weapon, ItemType.Amplifier)] public Sprite sprite;
    public string itemName;

    #region Weapon
    [ConditionalField("type", ItemType.Weapon)] public BulletSpawnerData weaponData;
    #endregion

    #region Amplifier
    [ConditionalField("type", ItemType.Amplifier)] public PlayerStats playerStat;
    [ConditionalField("type", ItemType.Amplifier)] public float ampValue;
    
    #endregion

    #region ExpShard
    [ConditionalField("type", ItemType.ExpShard)] public float expValue;
    #endregion
}
