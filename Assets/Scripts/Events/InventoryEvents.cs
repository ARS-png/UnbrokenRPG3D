using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<InventoryItemSO, int, PickUpItem> onItemAddedToInventory;

    public void AddItemToInventory(InventoryItemSO itemSO, int count, PickUpItem itemToDelete) => onItemAddedToInventory?.Invoke(itemSO, count, itemToDelete);
}
