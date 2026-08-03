using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private GameObject weaponInventory;
    [SerializeField] private GameObject amplifiersInventory;
    [SerializeField] private GameObject inventorySlot;
    [SerializeField] private Sprite weaponPlaceholderIcon;
    [SerializeField] private Sprite amplifierPlaceholderIcon;
    private List<GameObject> weaponSlots = new();
    private List<GameObject> amplifierSlots = new();
    private int currentWeaponSlot = 0;
    private int currentAmplifierSlot = 0;

    private void Start()
    {
        //Coroutine istead of for in Start ??
        for (int i = 0; i < PlayerManager.PlayerStats.maxWeaponNum; i++)
        {
            GameObject newInventorySlot = Instantiate(inventorySlot, weaponInventory.transform);
            newInventorySlot.GetComponent<Image>().sprite = weaponPlaceholderIcon;
            weaponSlots.Add(newInventorySlot);
        }

        for (int i = 0; i < PlayerManager.PlayerStats.maxAmplifiersNum; i++)
        {
            GameObject newInvetorySlot = Instantiate(inventorySlot, amplifiersInventory.transform);
            newInvetorySlot.GetComponent<Image>().sprite = amplifierPlaceholderIcon;
            amplifierSlots.Add(newInvetorySlot);
        }
    }

    public void AddItem(CollectableItemController item)
    {
        if (item.Data.type == ItemType.Weapon)
        {
            weaponSlots[currentWeaponSlot].transform.GetChild(0).GetComponent<Image>().sprite = item.Data.sprite;
        }
    }
}
