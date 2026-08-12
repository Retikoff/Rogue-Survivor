using System;
using UnityEngine;

public class CollectableItemController : MonoBehaviour
{
    [SerializeField] private CollectableItemData data;
    [HideInInspector] public CollectableItemData Data;

    private void Start()
    {
        Data = Instantiate(data);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Inventory.CollectItem(this);
        Destroy(this.gameObject);
    }
}
