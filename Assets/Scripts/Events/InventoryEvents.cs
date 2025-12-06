using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<InventoryItemSO, int, PickUpItem> onItemAddedToInventory;

    public event Action onInventoryPanelHide;
    public event Action onInventoryPanelShow;

    public void AddItemToInventory(InventoryItemSO itemSO, int count, PickUpItem itemToDelete) => onItemAddedToInventory?.Invoke(itemSO, count, itemToDelete);

    public void CloseInventoryPanel() => onInventoryPanelHide?.Invoke();   
    public void ShowInventoryPanel() => onInventoryPanelShow?.Invoke(); 
}
