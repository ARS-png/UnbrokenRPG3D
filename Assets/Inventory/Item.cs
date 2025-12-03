using UnityEngine;

public class Item 
{
    public InventoryItemSO inventoryItemSO;

    public int amount;
    public string itemType;
    public string itemId;

    public Item(InventoryItemSO inventoryItemSO)
    {
        this.inventoryItemSO = inventoryItemSO;

    }
}
