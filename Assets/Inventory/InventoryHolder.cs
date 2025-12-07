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
        GameEventsManager.instance.inventoryEvents.onGetPrefabFromInventory += GetPrefabFromInventory;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onItemAddedToInventory -= AddToInventoryWithDestroy;
        GameEventsManager.instance.inventoryEvents.onGetPrefabFromInventory -= GetPrefabFromInventory;
    }

    private void AddToInventoryWithDestroy(InventoryItemSO itemSO, int count, PickUpItem itemToDelete)
    {
        if (InventorySystem.AddToInventory(itemSO, count))
        {
            var item = inventorySystem.GetSlotByData(itemSO);

            inventoryUISystem.UpdateUIButtonItem(item);

            Destroy(itemToDelete.gameObject);
        }
    }

    private void GetPrefabFromInventory(string itemId)
    {
        if (inventorySystem.GetPrefabFromInventory(itemId) != null)
        {
            Instantiate(inventorySystem.GetPrefabFromInventory(itemId), this.transform.position, this.transform.rotation, null);
            Debug.Log("Prefab instantiaited with id" + itemId);
        }
    }
}
