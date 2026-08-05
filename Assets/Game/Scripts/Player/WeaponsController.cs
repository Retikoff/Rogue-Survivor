using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponsController : MonoBehaviour
{
    private List<GameObject> slots = new();
    private int currentWeaponSlot = 0;

    private InputAction moveAction;

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

    public void ApplyItem(CollectableItemController item)
    {
        slots[currentWeaponSlot].GetComponent<BulletSpawnerController>().Data = item.Data.weaponData; //боже что это (я плохо поспал когда писал это)
        currentWeaponSlot++;
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
}
