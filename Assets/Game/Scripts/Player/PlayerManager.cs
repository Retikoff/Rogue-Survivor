using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    private int experience;

    public void Startup()
    {
        Debug.Log("Player manager starting..,");

        status = ManagerStatus.Started;
    }

    public void AddExp(int amount)
    {
        experience += amount;
    }
}
