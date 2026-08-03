using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField] private InventoryUIManager inventoryUI;
    private List<string> items; // not string

    public ManagerStatus status {get; private set;}
    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        items = new List<string>();

        status = ManagerStatus.Started;
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
    }
}
