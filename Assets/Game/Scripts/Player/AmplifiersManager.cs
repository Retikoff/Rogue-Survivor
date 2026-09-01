using System.Collections.Generic;
using UnityEngine;

public class AmplifiersManager : MonoBehaviour, IGameManager
{
    private List<CollectableItemController> items = new();
    public List<CollectableItemController> GetItems() => items;
    public int GetItemsCount() => items.Count;
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
        switch (item.Data.amplifierData.playerStat)
        {
            case PlayerStats.Health:
                Managers.Player.PlayerStats.HealthAmplifier = item.Data.amplifierData.baseValue;
                break;
            case PlayerStats.Speed:
                Managers.Player.PlayerStats.speedAmplifier = item.Data.amplifierData.baseValue;
                break;
            
        }
        CurrentAmplifierSlot++;
    }
}
