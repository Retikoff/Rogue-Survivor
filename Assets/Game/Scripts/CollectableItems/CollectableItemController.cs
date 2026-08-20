using System;
using System.Data;
using UnityEngine;

public class CollectableItemController : MonoBehaviour
{
    [SerializeField] private CollectableItemData data;
    [HideInInspector] public CollectableItemData Data;

    private void Start()
    {
        Data = Instantiate(data);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null)
        {
            Managers.Inventory.CollectItem(this);
            Destroy(this.gameObject);
        }
    }

    // upgrade() должен быть тут
    public void Upgrade(WeaponStatValuePair levelUpgrade)
    {
        switch (levelUpgrade.Stat)
        {
            case WeaponStats.BaseDamage:
                Data.weaponData.baseDamage += levelUpgrade.Value;
                break;
            case WeaponStats.Area:
                Data.weaponData.area += levelUpgrade.Value;
                break;
            case WeaponStats.Cooldown:
                Data.weaponData.cooldown -= levelUpgrade.Value;
                break;
            case WeaponStats.Speed:
                Data.weaponData.speed += levelUpgrade.Value;
                break;
            case WeaponStats.ProjectileAmount:
                Data.weaponData.projectileAmount += levelUpgrade.Value;
                break;
            case WeaponStats.Duration:
                Data.weaponData.duration += levelUpgrade.Value;
                break;
            case WeaponStats.Pierce:
                Data.weaponData.pierce += (int)levelUpgrade.Value;
                break;
            case WeaponStats.Knockback:
                Data.weaponData.knockback += levelUpgrade.Value;
                break;
            case WeaponStats.CritMulti:
                Data.weaponData.critMulti += levelUpgrade.Value;
                break;
            case WeaponStats.CritChance:
                Data.weaponData.critChance += levelUpgrade.Value;
                break;
            case WeaponStats.BlockByWalls:
                Data.weaponData.blockByWalls = levelUpgrade.Value > 0;
                break;
        }
    }

    // для List.Remove() в GameRules
    public override bool Equals(object obj)
    {
        if (obj is CollectableItemController other)
        {
            return this.Data.itemName == other.Data.itemName;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Data.itemName);
    }

    // для глубокой копии List'a
    public CollectableItemController Clone()
    {
        return new CollectableItemController {Data = Instantiate(this.Data)};
    }
}
