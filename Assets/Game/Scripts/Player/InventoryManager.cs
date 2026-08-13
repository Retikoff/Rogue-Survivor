using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerExperienceController))]
public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField] private InventoryUIManager inventoryUI;
    private PlayerExperienceController playerExpController;
    private List<string> items; // сделать из нее журнал действий (например подобрал большой опыт, поднял пистолет, поднял книгу на скорость)

    public ManagerStatus status {get; private set;}

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        status = ManagerStatus.Started;
    }

    private void Start()
    {
        playerExpController = GetComponent<PlayerExperienceController>();
    }

    public void CollectItem(CollectableItemController item)
    {
        ApplyItem(item);
        Managers.GameRules.DeleteItemFromAccessible(item);
    }

    private void ApplyItem(CollectableItemController item)
    {
        if (item.Data.type == ItemType.Weapon)
        {
            Managers.Weapons.ApplyItem(item);
            inventoryUI.AddWeapon(item);
        }   

        if (item.Data.type == ItemType.Amplifier)
        {
            Managers.Amplifiers.ApplyItem(item);
            inventoryUI.AddAmplifier(item);
        }

        if (item.Data.type == ItemType.ExpShard)
        {
            playerExpController.GetExp(item.Data.expValue);
        }
    }
}
