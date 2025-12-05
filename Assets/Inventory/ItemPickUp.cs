using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public float pickUpRadius = 1;
    public InventoryItemSO ItemSO;

    private SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider> ();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;   
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (inventory == null) 
        {
            Debug.LogError("");
            return;
        }
        else
        {
            if (inventory.InventorySystem.AddToInventory(ItemSO, 1))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
