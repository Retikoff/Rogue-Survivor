using UnityEngine;

public class GameRulesManager : MonoBehaviour, IGameManager
{
    [SerializeField] private GameRulesData gameRules;
    public static GameRulesData GameRules;
    public ManagerStatus status {get; private set;}

    private void Awake()
    {
        GameRules = gameRules;
    }

    public void Startup()
    {
        Debug.Log("GameRules manager starting..,");

        status = ManagerStatus.Started;
    }
}
