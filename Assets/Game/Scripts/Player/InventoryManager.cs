using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    private List<string> items; // not string

    public ManagerStatus status {get; private set;}
    public void Startup()
    {
        Debug.Log("Inventory manager starting...");

        items = new List<string>();

        status = ManagerStatus.Started;
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (string item in items)
        {
            itemDisplay += item + " ";
        }
        Debug.Log(itemDisplay);
    }

    public void AddItem(string name)
    {
        items.Add(name);

        DisplayItems();
    }
}
