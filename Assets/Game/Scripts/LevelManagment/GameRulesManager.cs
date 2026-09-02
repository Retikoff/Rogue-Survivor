using UnityEngine;

public class GameRulesManager : MonoBehaviour, IGameManager
{
    [SerializeField] private GameRulesData gameRules;
    [HideInInspector] public GameRulesData Data;
    public ManagerStatus status {get; private set;}

    private void Awake()
    {
        Data = Instantiate(gameRules);
    }

    public void Startup()
    {
        Debug.Log("GameRules manager starting..,");

        status = ManagerStatus.Started;
    }

    public void DeleteItemFromAccessible(CollectableItemController item)
    {
        if (item.Data.type == ItemType.Weapon)
        {
            Data.accessibleWeapons.Remove(item);
        }

        if (item.Data.type == ItemType.Amplifier)
        {
            Data.accessibleAmplifiers.Remove(item);
       }
    }
}
