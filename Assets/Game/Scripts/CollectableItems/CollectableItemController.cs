using System;
using UnityEngine;

public class CollectableItemController : MonoBehaviour
{
    public CollectableItemData Data;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Inventory.CollectItem(this);
        Destroy(this.gameObject);
    }
}
