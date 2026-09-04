using System.Collections;
using UnityEngine;

public class GameRulesManager : MonoBehaviour, IGameManager
{
    [SerializeField] private GameRulesData gameRules;
    [HideInInspector] public GameRulesData Data;
    public ManagerStatus status {get; private set;}

    private void Awake()
    {
        Data = Instantiate(gameRules);
        foreach (CollectableItemController controller in Data.accessibleWeapons)
        {
            controller.InitializeData();
        }

        foreach (CollectableItemController controller in Data.accessibleAmplifiers)
        {
            controller.InitializeData();
        }
    }

    private void Start()
    {
        StartCoroutine(CheckAfterDelay());
    }

    private IEnumerator CheckAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(Data.accessibleWeapons[0].Data);
        Debug.Log(Data.accessibleWeapons[1].Data);
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
