using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    [SerializeField] private PlayerStatsData playerStats;
    public static PlayerStatsData PlayerStats;
    public ManagerStatus status {get; private set;}

    private void Awake()
    {
        //Instantiate it so when i change it on runtime it wont stay after exit play mode
        PlayerStats = Instantiate(playerStats);
    }

    public void Startup()
    {
        Debug.Log("Player manager starting..,");

        status = ManagerStatus.Started;
    }
}
