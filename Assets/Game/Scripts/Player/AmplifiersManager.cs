using System.Collections.Generic;
using UnityEngine;

public class AmplifiersManager : MonoBehaviour, IGameManager
{
    private List<CollectableItemController> items = new();
    public List<CollectableItemController> GetItems() => items;
    public int GetItemsCount() => items.Count;
    private int currentAmplifierSlot = 0;
    public int CurrentAmplifierSlot { get; private set; }

    public ManagerStatus status {get; private set;}

    public void Startup()
    {
        Debug.Log("Amplifiers manager starting...");

        status = ManagerStatus.Started;
    }

    public void ApplyItem(CollectableItemController item)
    {
        items.Add(item);
        ApplyToPlayer(item);
        CurrentAmplifierSlot++;
    }

    private void ApplyToPlayer(CollectableItemController item)
    {
        switch (item.Data.playerStat)
        {
            case PlayerStats.Health:
                PlayerManager.PlayerStats.HealthAmplifier = item.Data.ampValue;
                break;
            case PlayerStats.Speed:
                PlayerManager.PlayerStats.speedAmplifier = item.Data.ampValue;
                break;
            
        }
        CurrentAmplifierSlot++;
    }
}
