using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    [SerializeField] private PlayerStatsData playerStats;
    public static PlayerStatsData PlayerStats;
    public ManagerStatus status {get; private set;}

    private void Awake()
    {
        PlayerStats = playerStats;
    }

    public void Startup()
    {
        Debug.Log("Player manager starting..,");

        status = ManagerStatus.Started;
    }
}
