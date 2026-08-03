using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItemData", menuName = "Scriptable Objects/CollectableItemData")]
public class CollectableItemData : ScriptableObject
{
    public ItemType type;

    public Sprite sprite;
    public string itemName;

    [ConditionalField("type", ItemType.Weapon)] public float damage;
    [ConditionalField("type", ItemType.Weapon)] public float firingRate;
    [ConditionalField("type", ItemType.Weapon)] public GameObject projectile;
}
