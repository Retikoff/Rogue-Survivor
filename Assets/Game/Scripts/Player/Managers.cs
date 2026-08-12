using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(UpgradeManager))]
[RequireComponent(typeof(WeaponsManager))]
[RequireComponent(typeof(AmplifiersManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager Player {get; private set;}
    public static InventoryManager Inventory {get; private set;}
    public static GameRulesManager GameRules {get; private set;}
    public static UpgradeManager Upgrade {get; private set;}
    public static WeaponsManager Weapons {get; private set;}
    public static AmplifiersManager Amplifiers {get; private set;}

    private List<IGameManager> startSequence;

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        //GameRules = GameObject.Find("GameRulesManager").GetComponent<GameRulesManager>();
        Upgrade = GetComponent<UpgradeManager>();
        Weapons = GetComponent<WeaponsManager>();
        Amplifiers = GetComponent<AmplifiersManager>();

        startSequence = new List<IGameManager>();
        startSequence.Add(Player);
        startSequence.Add(Inventory);
        //startSequence.Add(GameRules);
        startSequence.Add(Upgrade);
        startSequence.Add(Weapons);
        startSequence.Add(Amplifiers);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
            }
            yield return null;
        }

        Debug.Log("All managers started up");
    }
}
