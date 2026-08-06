using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItemData", menuName = "Scriptable Objects/CollectableItemData")]
public class CollectableItemData : ScriptableObject
{
    public ItemType type;

    [ConditionalField("type", ItemType.Weapon, ItemType.Amplifier)] public Sprite sprite;
    public string itemName;

    [ConditionalField("type", ItemType.Weapon)] public BulletSpawnerData weaponData;

    [ConditionalField("type", ItemType.ExpShard)] public float expValue;
}
