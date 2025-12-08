using UnityEngine;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    [SerializeField] protected InventorySystem inventorySystem;

    [SerializeField] private InventoryUISystem inventoryUISystem;

    public InventorySystem InventorySystem => inventorySystem;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onItemAddedToInventory += AddToInventoryWithDestroy;
        GameEventsManager.instance.inventoryEvents.onRemoveFromInventory += RemoveFromInventory;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onItemAddedToInventory -= AddToInventoryWithDestroy;
        GameEventsManager.instance.inventoryEvents.onRemoveFromInventory -= RemoveFromInventory;
    }

    private void AddToInventoryWithDestroy(InventoryItemSO itemSO, int amount, PickUpItem itemToDelete)
    {
        if (InventorySystem.AddToInventory(itemSO, amount))
        {
            var item = inventorySystem.GetSlotByData(itemSO);

            inventoryUISystem.UpdateUIButtonItem(item);

            Destroy(itemToDelete.gameObject);
        }
    }

    protected virtual void RemoveFromInventory(string itemId, int amountToDelete)
    {
        InventorySlot item = inventorySystem.GetSlotByItemId(itemId);

        if (item == null)
        {
            Debug.LogError("Iventory Item is null");
            return; 
        }

        if (InventorySystem.RemoveFromInventory(item.ItemSO, amountToDelete)) //ItemID после удаления становится null
        {
            Debug.Log("Item is removed form iventory ewith ID: ");
            inventoryUISystem.UpdateUIButtonItem(item);
            
        }
    }
}
