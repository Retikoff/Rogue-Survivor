using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItemData", menuName = "Scriptable Objects/CollectableItemData")]
public class CollectableItemData : ScriptableObject
{
    public ItemType type;

    [ConditionalField("type", ItemType.Weapon, ItemType.Amplifier)] public Sprite sprite;
    public string itemName;

    #region Weapon
    [ConditionalField("type", ItemType.Weapon)] public WeaponData weaponData;
    [ConditionalField("type", ItemType.Weapon)] public string weaponControllerName;
    #endregion

    #region Amplifier
    [ConditionalField("type", ItemType.Amplifier)] public PlayerStats playerStat;
    [ConditionalField("type", ItemType.Amplifier)] public float ampValue;
    
    #endregion

    #region ExpShard
    [ConditionalField("type", ItemType.ExpShard)] public float expValue;
    #endregion

    #region Weapon and Amplifier

    [ConditionalField("type", ItemType.Weapon, ItemType.Amplifier)] public ItemProgression progression;
    [HideInInspector] public int level = 0;
    [ConditionalField("type", ItemType.Weapon, ItemType.Amplifier)] public String itemDescription;

    #endregion

    public IWeapon CreateWeaponController(GameObject target)
    {
        if (string.IsNullOrEmpty(weaponControllerName))
        {
            return null;
        }

        Component component = target.AddComponent(Type.GetType(weaponControllerName));
        return component as IWeapon;
    }
}
