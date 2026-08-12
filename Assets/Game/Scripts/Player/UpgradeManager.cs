using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerExperienceController))]
public class UpgradeManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    private GameObject upgradeView;
    private GameObject upgradeGrid;
    [SerializeField] private GameObject upgradeSlot;
    private List<Sprite> sprites = new();
    private List<string> itemNames = new();
    private List<string> itemDescriptions = new();

    private void Start()
    {
        upgradeView = GameObject.Find("InventoryProgressCanvas").transform.Find("UpgradeView").gameObject;
        upgradeGrid = upgradeView.transform.GetChild(0).gameObject;
        GetComponent<PlayerExperienceController>().OnPlayerLevelUped += Upgrade;
    }

    public void Startup()
    {
        Debug.Log("Upgrade Manager starting..,");

        status = ManagerStatus.Started;
    }

    // несколько уровней сразу
    public void Upgrade()
    {
        //остановить игру
        // что улучшать/добавлять

        // from existing
        CollectableItemController weaponToUpgrade = Managers.Weapons.GetItems()[UnityEngine.Random.Range(0, Managers.Weapons.CurrentWeaponSlot)];
        sprites.Add(weaponToUpgrade.Data.sprite);
        itemNames.Add(weaponToUpgrade.Data.itemName);
        itemDescriptions.Add(weaponToUpgrade.Data.itemDescription);

        //показать
        for (int i = 0; i < sprites.Count; i++)
        {
            GameObject newUpgradeSlot = Instantiate(upgradeSlot, upgradeGrid.transform);
            newUpgradeSlot.transform.GetChild(0).GetComponent<Image>().sprite = sprites[i];
            newUpgradeSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemNames[i];
            newUpgradeSlot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemDescriptions[i];
        }

        //улучшить


        upgradeView.SetActive(true);
    }
}
