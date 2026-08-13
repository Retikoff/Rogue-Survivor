using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerExperienceController))]
public class UpgradeManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    private GameObject upgradeView;
    private GameObject upgradeGrid;
    [SerializeField] private GameObject upgradeSlot;
    private List<CollectableItemController> upgradableItems = new();

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
        Time.timeScale = 0f;
        // что улучшать/добавлять

        int currentWeaponsNum = Managers.Weapons.GetItemsCount();
        int maxWeaponNum = PlayerManager.PlayerStats.maxWeaponNum;
        List<CollectableItemController> weaponsToUpgrade = Managers.Weapons.GetItems();
        

        int currentAmplifiersNum = Managers.Amplifiers.GetItemsCount();
        int maxAmplifiersNum = PlayerManager.PlayerStats.maxAmplifiersNum;
        List<CollectableItemController> amplifiersToUpgrade = Managers.Amplifiers.GetItems();

        // gold if weaponsToUpgrade.Count == 0 && amplifiersToUpgrade.Count == 0
        while (upgradableItems.Count < 3)
        {
            if (UnityEngine.Random.Range(0,100) > 50)
            {
                //upgrade / take weapon

                if (currentWeaponsNum == 0)
                {
                    // take from accesible
                }
                else if (currentWeaponsNum > 0 && currentWeaponsNum < maxWeaponNum && weaponsToUpgrade.Count > 0)
                {
                    if (UnityEngine.Random.Range(0, 100) > 60)
                    {
                        CollectableItemController weaponToUpgrade = weaponsToUpgrade[UnityEngine.Random.Range(0, weaponsToUpgrade.Count)];
                        weaponsToUpgrade.Remove(weaponToUpgrade);
                        upgradableItems.Add(weaponToUpgrade);
                    }
                    else
                    {
                        // take from accesible
                    }
                }
                else if (currentWeaponsNum == maxWeaponNum && weaponsToUpgrade.Count > 0)
                {
                    CollectableItemController weaponToUpgrade = weaponsToUpgrade[UnityEngine.Random.Range(0, weaponsToUpgrade.Count)];
                    weaponsToUpgrade.Remove(weaponToUpgrade);
                    upgradableItems.Add(weaponToUpgrade);
                }
            }
            else
            {
                //upgrade / take amplifier
    
                if (currentAmplifiersNum == 0)
                {
                    // take from accesible
                }
                else if (currentAmplifiersNum > 0 && currentAmplifiersNum < maxAmplifiersNum && amplifiersToUpgrade.Count > 0)
                {
                    if (UnityEngine.Random.Range(0, 100) > 60)
                    {
                        CollectableItemController amplifierToUpgrade = amplifiersToUpgrade[UnityEngine.Random.Range(0, amplifiersToUpgrade.Count)];
                        amplifiersToUpgrade.Remove(amplifierToUpgrade);
                        upgradableItems.Add(amplifierToUpgrade);
                    }
                    else
                    {
                        //take from accesible
                    }
                }
                else if (currentAmplifiersNum == maxAmplifiersNum && amplifiersToUpgrade.Count > 0)
                {
                    CollectableItemController amplifierToUpgrade = amplifiersToUpgrade[UnityEngine.Random.Range(0, amplifiersToUpgrade.Count)];
                    amplifiersToUpgrade.Remove(amplifierToUpgrade);
                    upgradableItems.Add(amplifierToUpgrade);
                }
            }
        }

        //показать
        for (int i = 0; i < upgradableItems.Count; i++)
        {
            GameObject newUpgradeSlot = Instantiate(upgradeSlot, upgradeGrid.transform);
            int index = i;
            newUpgradeSlot.transform.GetComponent<Button>().onClick.AddListener(() => UpgradeItem(upgradableItems[index]));
            newUpgradeSlot.transform.GetChild(0).GetComponent<Image>().sprite = upgradableItems[i].Data.sprite;
            newUpgradeSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = upgradableItems[i].Data.itemName;
            newUpgradeSlot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = upgradableItems[i].Data.itemDescription;
        }

        //улучшить


        upgradeView.SetActive(true);
    }

    public void UpgradeItem(CollectableItemController item)
    {
        Debug.Log("weapon to upgrade: " + item.Data.itemName);

        item.Upgrade();

        ResumeGame();
    }

    private void ResumeGame()
    {
        upgradeView.SetActive(false);
        foreach (Transform child in upgradeView.transform)
        {
            Destroy(child.gameObject);
        }

        Time.timeScale = 1f;
    }
}
