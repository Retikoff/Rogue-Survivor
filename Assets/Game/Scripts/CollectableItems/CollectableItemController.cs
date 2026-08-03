using System;
using UnityEngine;

public class CollectableItemController : MonoBehaviour
{
    [SerializeField] public CollectableItemData Data;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Inventory.AddItem(this);
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
}
