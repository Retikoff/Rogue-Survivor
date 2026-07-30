using UnityEngine;

public class CollectableItemController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Inventory.AddItem("Test pistol");
        Destroy(this.gameObject);
    }
}
