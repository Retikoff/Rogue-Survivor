using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField] private InventoryUIManager inventoryUI;
    private PlayerExperienceController playerExpController;
    private WeaponsController weaponsController;
    private List<string> items; // сделать из нее журнал действий (например подобрал большой опыт, поднял пистолет, поднял книгу на скорость)
    private List<GameObject> amplifierSlots = new(); //amplifiersController?
    private int currentAmplifierSlot = 0;

    public ManagerStatus status {get; private set;}

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        status = ManagerStatus.Started;
    }

    private void Start()
    {
        weaponsController = GetComponent<WeaponsController>();
        playerExpController = GetComponent<PlayerExperienceController>();
    }

    public void CollectItem(CollectableItemController item)
    {
        ApplyItem(item);
    }

    private void ApplyItem(CollectableItemController item)
    {
        if (item.Data.type == ItemType.Weapon)
        {
            weaponsController.ApplyItem(item);
            inventoryUI.AddWeapon(item);
        }   

        //что такое amplifier?

        if (item.Data.type == ItemType.ExpShard)
        {
            playerExpController.GetExp(item.Data.expValue);
        }
    }
}
