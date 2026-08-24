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
    // string не нравицца
    [ConditionalField("type", ItemType.Weapon)] public string weaponControllerName;
    [ConditionalField("type", ItemType.Weapon)] public WeaponProgressionData weaponProgression;
    /*
    usage: some cs script: 
    WeaponStatValuePair upgradePair = progresion.GetValue(0, 0);
    */
    #endregion

    #region Amplifier
    [ConditionalField("type", ItemType.Amplifier)] public AmplifierData amplifierData;
    
    #endregion

    #region Weapon and Amplifier

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
