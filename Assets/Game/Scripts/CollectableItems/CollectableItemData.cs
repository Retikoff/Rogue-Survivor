using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItemData", menuName = "Scriptable Objects/CollectableItemData")]
public class CollectableItemData : ScriptableObject
{
    public ItemType type;

    public Sprite sprite;
    public string itemName;

    [ConditionalField("type", ItemType.Weapon)] public BulletSpawnerData weaponData;
}
