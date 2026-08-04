using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField] private InventoryUIManager inventoryUI;
    private List<string> items; // not string
    private List<GameObject> weaponSlots = new();
    private List<GameObject> amplifierSlots = new();
    private int currentWeaponSlot = 0;
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
        for (int i = 0; i < PlayerManager.PlayerStats.maxWeaponNum; i++)
        {
            GameObject newSlot = new GameObject("Weapon Slot " + i, typeof(BulletSpawnerController));
            newSlot.transform.SetParent(this.transform);
            weaponSlots.Add(newSlot);
        }
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
            weaponSlots[currentWeaponSlot].GetComponent<BulletSpawnerController>().Data = item.Data.weaponData; //боже что это (я плохо поспал когда писал это)
            currentWeaponSlot++;
        }

        //что такое amplifier?
    }
}
