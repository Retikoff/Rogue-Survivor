using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponsManager : MonoBehaviour, IGameManager
{
    private List<GameObject> slots = new();
    private List<CollectableItemController> items = new();
    private int currentWeaponSlot = 0;
    public int CurrentWeaponSlot {get; private set;}

    private InputAction moveAction;

    public ManagerStatus status {get; private set;}

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Start()
    {
        for (int i = 0; i < PlayerManager.PlayerStats.maxWeaponNum; i++)
        {
            GameObject newSlot = new GameObject("Weapon Slot " + i, typeof(BulletSpawnerController));
            newSlot.transform.SetParent(this.transform);
            slots.Add(newSlot);
        }
    }

    public void Startup()
    {
        Debug.Log("Weapons manager starting...");

        status = ManagerStatus.Started;
    }

    public void ApplyItem(CollectableItemController item)
    {
        // if currenWeaponSlot > slots.Count
        slots[CurrentWeaponSlot].GetComponent<BulletSpawnerController>().Data = item.Data.weaponData; //боже что это (я плохо поспал когда писал это)
        items.Add(item);
        CurrentWeaponSlot++;
    }

    public void Update()
    {
        if (moveAction.WasPerformedThisFrame())
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            float rotationValue = Vector2.SignedAngle(Vector2.right, moveValue);

            foreach(var slot in slots)
            {
                slot.transform.eulerAngles = new Vector3(0f,0f, rotationValue);
            }
        }
    }

    public List<CollectableItemController> GetItems() => items;
}
