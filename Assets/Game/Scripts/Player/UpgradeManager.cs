using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    private GameObject upgradeView;
    private GameObject upgradeGrid;
    [SerializeField] private GameObject upgradeSlot;
    private List<CollectableItemController> upgradableItems = new();
    private int currentWeaponsNum;
    private int maxWeaponsNum;
    private List<CollectableItemController> weaponsToUpgrade = new();
    private int countWeaponsToUpgrade = 0;
    private int countNewWeapons = 0;
    private int countAmplifiersToUpgrade = 0;
    private int countNewAmplifiers = 0;
    private List<CollectableItemController> accessibleWeapons = new();
    private int currentAmplifiersNum;
    private int maxAmplifiersNum;
    private List<CollectableItemController> amplifiersToUpgrade = new();
    private List<CollectableItemController> accessibleAmplifiers = new();

    private void Start()
    {
        upgradeView = GameObject.Find("InventoryProgressCanvas").transform.Find("UpgradeView").gameObject;
        upgradeGrid = upgradeView.transform.GetChild(0).gameObject;
        Managers.ExperienceManager.OnPlayerLevelUped += Upgrade;
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
        StopGame();
        // что улучшать/добавлять

        GetWeaponsToUpgradeAndAccessible();
        GetAmplifiersToUpgradeAndAccessible();

        //
        // gold if weaponsToUpgrade.Count == 0 && amplifiersToUpgrade.Count == 0
        //

        
        while (upgradableItems.Count < 3) //??
        {
            //if (UnityEngine.Random.Range(0,100) > 50)
            //{
                //upgrade / take weapon

                if (currentWeaponsNum == 0)
                {
                    TakeWeaponFromAccessible();
                }
                else if (currentWeaponsNum > 0 && currentWeaponsNum < maxWeaponsNum)
                {
                    if (UnityEngine.Random.Range(0, 100) > 60)
                    {
                        TakeWeaponForUpgrade();
                    }
                    else
                    {
                        TakeWeaponFromAccessible();
                    }
                }
                else if (currentWeaponsNum >= maxWeaponsNum)
                {
                    TakeWeaponForUpgrade();
                }
           //}
            // else
            // {
            //     //upgrade / take amplifier
    
            //     if (currentAmplifiersNum == 0)
            //     {
            //         TakeAmplifierFromAccessible();
            //     }
            //     else if (currentAmplifiersNum > 0 && currentAmplifiersNum < maxAmplifiersNum && amplifiersToUpgrade.Count > 0)
            //     {
            //         if (UnityEngine.Random.Range(0, 100) > 60)
            //         {
            //             TakeAmplifierForUpgrade();
            //         }
            //         else
            //         {
            //             TakeAmplifierFromAccessible();
            //         }
            //     }
            //     else if (currentAmplifiersNum >= maxAmplifiersNum && amplifiersToUpgrade.Count > 0)
            //     {
            //         TakeAmplifierForUpgrade();
            //     }
            // }
        }

        
        //показать
        for (int i = 0; i < upgradableItems.Count; i++)
        {
            GameObject newUpgradeSlot = Instantiate(upgradeSlot, upgradeGrid.transform);
            int index = i;
            //улучшить
            newUpgradeSlot.transform.GetChild(0).GetComponent<Image>().sprite = upgradableItems[i].Data.sprite;
            newUpgradeSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = upgradableItems[i].Data.itemName;
            if (upgradableItems[i].Data.level > 0)
            {
                // upgrade stat
                if (upgradableItems[i].Data.type == ItemType.Weapon)
                {
                    WeaponStatValueRow levelRow = upgradableItems[i].Data.weaponProgression.GetRow(upgradableItems[i].Data.level + 1);
                    WeaponStatValuePair levelUpgrade = levelRow.Pairs[UnityEngine.Random.Range(0, levelRow.Pairs.Count)];
                    newUpgradeSlot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{levelUpgrade.Stat.ToString()}: {levelUpgrade.Value.ToString()}";
                    newUpgradeSlot.transform.GetComponent<Button>().onClick.AddListener(() => UpgradeItem(upgradableItems[index], levelUpgrade));
                }

                if (upgradableItems[i].Data.type == ItemType.Amplifier)
                {
                    float levelUpgrade = upgradableItems[i].Data.amplifierData.levelUpgrades[upgradableItems[i].Data.level + 1];
                    newUpgradeSlot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{upgradableItems[i].Data.amplifierData.playerStat}: {levelUpgrade}";
                    newUpgradeSlot.transform.GetComponent<Button>().onClick.AddListener(() => UpgradeItem(upgradableItems[index]));
                }
            }
            else
            {
                newUpgradeSlot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = upgradableItems[i].Data.itemDescription;
                newUpgradeSlot.transform.GetComponent<Button>().onClick.AddListener(() => AddItem(upgradableItems[index]));
            }
        }

        upgradeView.SetActive(true);
    }

    public void UpgradeItem(CollectableItemController item, WeaponStatValuePair levelUpgrade)
    {
        Debug.Log("weapon to upgrade: " + item.Data.itemName);

        item.Upgrade(levelUpgrade);

        ResumeGame();
    }

    public void UpgradeItem(CollectableItemController item)
    {
        Debug.Log("Amplifier to upgrade: " + item.Data.itemName);

        item.Upgrade();

        ResumeGame();
    }

    public void AddItem(CollectableItemController item)
    {
        Managers.Inventory.CollectItem(item);

        ResumeGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        upgradeView.SetActive(false);
        foreach (Transform child in upgradeGrid.transform)
        {
            Destroy(child.gameObject);
        }


        upgradableItems.Clear();

        Time.timeScale = 1f;
    }

    #region Weapons

    private void GetWeaponsToUpgradeAndAccessible()
    {
        currentWeaponsNum = Managers.Weapons.GetItemsCount();
        maxWeaponsNum = Managers.Player.PlayerStats.maxWeaponNum;
        weaponsToUpgrade = Managers.Weapons.GetItems();
        foreach (CollectableItemController weapon in weaponsToUpgrade)
        {
            if (weapon.Data.level >= weapon.Data.weaponData.maxLevel)
            {
                weaponsToUpgrade.Remove(weapon);
            }
        }
        accessibleWeapons = Managers.GameRules.Data.accessibleWeapons; // ссылка
    }

    private void TakeWeaponFromAccessible()
    {
        CollectableItemController newWeapon = accessibleWeapons[UnityEngine.Random.Range(0, accessibleWeapons.Count)];
        
        if (countNewWeapons < accessibleWeapons.Count)
        {
            while (upgradableItems.Contains(newWeapon))
            {
                newWeapon = accessibleWeapons[UnityEngine.Random.Range(0, accessibleWeapons.Count)];
            }
            upgradableItems.Add(newWeapon);
            countNewWeapons++;
        }
    }

    private void TakeWeaponForUpgrade()
    {
        CollectableItemController weaponToUpgrade = weaponsToUpgrade[UnityEngine.Random.Range(0, weaponsToUpgrade.Count)];
        
        if (countWeaponsToUpgrade < currentWeaponsNum)
        {
            while (upgradableItems.Contains(weaponToUpgrade)) 
            {
                weaponToUpgrade = weaponsToUpgrade[UnityEngine.Random.Range(0, weaponsToUpgrade.Count)];
            }

            upgradableItems.Add(weaponToUpgrade);
            countWeaponsToUpgrade++;
        }
    }
    #endregion 

    #region Amplifiers
    private void GetAmplifiersToUpgradeAndAccessible()
    {
        currentAmplifiersNum = Managers.Amplifiers.GetItemsCount();
        maxAmplifiersNum = Managers.Player.PlayerStats.maxAmplifiersNum;
        amplifiersToUpgrade = Managers.Amplifiers.GetItems();
        foreach (CollectableItemController amplifier in amplifiersToUpgrade)
        {
            if (amplifier.Data.level >= amplifier.Data.amplifierData.maxLevel)
            {
                amplifiersToUpgrade.Remove(amplifier);
            }
        }
        accessibleAmplifiers = Managers.GameRules.Data.accessibleAmplifiers; // сслылка
    }
    private void TakeAmplifierFromAccessible()
    {

        CollectableItemController amplifierToUpgrade = accessibleAmplifiers[UnityEngine.Random.Range(0, accessibleAmplifiers.Count)];
        accessibleAmplifiers.Remove(amplifierToUpgrade);
        upgradableItems.Add(amplifierToUpgrade);
    }
    private void TakeAmplifierForUpgrade()
    {
        CollectableItemController amplifierToUpgrade = amplifiersToUpgrade[UnityEngine.Random.Range(0, amplifiersToUpgrade.Count)];
        amplifiersToUpgrade.Remove(amplifierToUpgrade);
        upgradableItems.Add(amplifierToUpgrade);
    }
    #endregion
}
