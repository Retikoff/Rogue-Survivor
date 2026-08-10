using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField] private InventoryUIManager inventoryUI;
    private PlayerExperienceController playerExpController;
    private WeaponsController weaponsController;
    private AmplifiersController amplifiersController;
    private List<string> items; // сделать из нее журнал действий (например подобрал большой опыт, поднял пистолет, поднял книгу на скорость)

    public ManagerStatus status {get; private set;}

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        status = ManagerStatus.Started;
    }

    private void Start()
    {
        weaponsController = GetComponent<WeaponsController>();
        amplifiersController = GetComponent<AmplifiersController>();
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

        if (item.Data.type == ItemType.Amplifier)
        {
            amplifiersController.ApplyItem(item);
            inventoryUI.AddAmplifier(item);
        }

        if (item.Data.type == ItemType.ExpShard)
        {
            playerExpController.GetExp(item.Data.expValue);
        }
    }
}
