using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponsManager : MonoBehaviour, IGameManager
{
    private List<GameObject> slots = new();
    private List<CollectableItemController> items = new();
    public List<CollectableItemController> GetItems() => items;
    public int GetItemsCount() => items.Count;
    private int currentWeaponSlot = 0;
    public int CurrentWeaponSlot {get; private set;}

    private InputAction moveAction;

    public ManagerStatus status {get; private set;}

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    public void Startup()
    {
        Debug.Log("Weapons manager starting...");

        status = ManagerStatus.Started;
    }

    public void ApplyItem(CollectableItemController item)
    {
        AddSlot();
        IWeapon weaponController = item.Data.CreateWeaponController(slots[CurrentWeaponSlot]);
        weaponController.WeaponData = item.Data.weaponData;
        items.Add(item);
        CurrentWeaponSlot++;
    }

    public void Update()
    {
        if (moveAction.WasPerformedThisFrame())
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            float rotationValue = Vector2.SignedAngle(Vector2.right, moveValue);

            for (int i = 0; i < slots.Count; i++)
            {
                if (items[i].Data.weaponData.isRotatable)
                {
                    slots[i].transform.eulerAngles = new Vector3(0f,0f, rotationValue);
                }
            }
        }
    }

    private void AddSlot()
    {
        GameObject newSlot = new GameObject("Weapon Slot " + CurrentWeaponSlot);
        newSlot.transform.position = transform.position;
        newSlot.transform.rotation = transform.rotation;
        newSlot.transform.SetParent(this.transform);
        slots.Add(newSlot);
    }
}
