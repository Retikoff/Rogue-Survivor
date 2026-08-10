using System.Collections.Generic;
using UnityEngine;

public class AmplifiersController : MonoBehaviour
{
    private List<CollectableItemController> slots = new();
    private int currentAmplifierSlot = 0;
    public void ApplyItem(CollectableItemController item)
    {
        slots.Add(item);
        ApplyToPlayer(item);
        currentAmplifierSlot++;
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
            
            /* это ужасно это не должно так выглядеть
                надо сделать события на которые подписать статы игрока
                чтобы при изменении их они обновлялись у игрока
                с другой стороны тогда у меня не будет такого доступа через Editor
                всё будет гвоздями прибито в коде
             */
        }
        currentAmplifierSlot++;
    }
}
