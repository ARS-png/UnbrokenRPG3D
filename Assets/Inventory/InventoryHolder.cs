using UnityEngine;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onItemAddedToInventory += AddToInventoryWithDestroy;
    }

    private void OnDisable()
    {
        
    }

    private void AddToInventoryWithDestroy(InventoryItemSO itemSO, int count, PickUpItem itemToDelete)
    {
        if (InventorySystem.AddToInventory(itemSO, count))
        {
            Destroy(itemToDelete.gameObject);
        }
    }
}
