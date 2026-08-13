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

    // upgrade() должен быть тут
    public void Upgrade()
    {

    }

    // для List.Remove() в GameRules
    public override bool Equals(object obj)
    {
        if (obj is CollectableItemController other)
        {
            return this.Data.itemName == other.Data.itemName;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Data.itemName);
    }

    // для глубокой копии List'a

    public CollectableItemController Clone()
    {
        return new CollectableItemController {Data = Instantiate(this.Data)};
    }
}
