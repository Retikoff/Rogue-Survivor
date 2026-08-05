using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField] private InventoryUIManager inventoryUI;
    private WeaponsController weaponsController;
    private List<string> items; // not string
    private List<GameObject> amplifierSlots = new(); //amplifiersController?
    private int currentAmplifierSlot = 0;

    public ManagerStatus status {get; private set;}

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        items = new List<string>();

        status = ManagerStatus.Started;
    }

    private void Start()
    {
        weaponsController = GetComponent<WeaponsController>();
    }

    private void DisplayItems() //возможно ненужный метод, лишняя переброска
    {
        string itemDisplay = "Items: ";
        foreach (string item in items)
        {
            itemDisplay += item + " ";
        }
        Debug.Log(itemDisplay);
    }

    public void AddItem(CollectableItemController item)
    {
        items.Add(item.Data.itemName);
        inventoryUI.AddItem(item);
        DisplayItems();
        ApplyItem(item);
    }

    private void ApplyItem(CollectableItemController item)
    {
        if (item.Data.type == ItemType.Weapon)
        {
            weaponsController.ApplyItem(item);
        }   

        //что такое amplifier?
    }
}
