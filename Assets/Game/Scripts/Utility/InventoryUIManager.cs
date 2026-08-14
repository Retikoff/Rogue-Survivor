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
        for (int i = 0; i < Managers.Player.PlayerStats.maxWeaponNum; i++)
        {
            GameObject newInventorySlot = Instantiate(inventorySlot, weaponInventory.transform);
            newInventorySlot.GetComponent<Image>().sprite = weaponPlaceholderIcon;
            weaponSlots.Add(newInventorySlot);
        }

        for (int i = 0; i < Managers.Player.PlayerStats.maxAmplifiersNum; i++)
        {
            GameObject newInvetorySlot = Instantiate(inventorySlot, amplifiersInventory.transform);
            newInvetorySlot.GetComponent<Image>().sprite = amplifierPlaceholderIcon;
            amplifierSlots.Add(newInvetorySlot);
        }
    }

    public void AddWeapon(CollectableItemController item)
    {
        if (item.Data.type != ItemType.Weapon)
        {
            Debug.LogError("Failed to add " + item.Data.type + " to weapons inventory");
            return;
        } 

        if (currentWeaponSlot == weaponSlots.Count)
        {
            AddWeaponSlot();
        }

        weaponSlots[currentWeaponSlot].transform.GetChild(0).GetComponent<Image>().sprite = item.Data.sprite;
        currentWeaponSlot++;
    }
    
    public void AddAmplifier(CollectableItemController item)
    {
        if (item.Data.type != ItemType.Amplifier)
        {
            Debug.LogError("Failed to add " + item.Data.type + " to amplifiers inventory");
            return;
        }

        if (currentAmplifierSlot == amplifierSlots.Count)
        {
            AddAmplifierSlot();
        }

        amplifierSlots[currentAmplifierSlot].transform.GetChild(0).GetComponent<Image>().sprite = item.Data.sprite;
        currentAmplifierSlot++;
    }

    private void AddWeaponSlot()
    {
        GameObject newInventorySlot = Instantiate(inventorySlot, weaponInventory.transform);
        newInventorySlot.GetComponent<Image>().sprite = weaponPlaceholderIcon;
        weaponSlots.Add(newInventorySlot);
    }

    private void AddAmplifierSlot()
    {
        GameObject newInvetorySlot = Instantiate(inventorySlot, amplifiersInventory.transform);
        newInvetorySlot.GetComponent<Image>().sprite = amplifierPlaceholderIcon;
        amplifierSlots.Add(newInvetorySlot);
    }
}
