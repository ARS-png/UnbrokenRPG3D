using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<InventoryItemSO, int, PickUpItem> onItemAddedToInventory;


    public event Action onInventoryPanelHide;
    public event Action onInventoryPanelShow;
    public event Action<string, int> onRemoveFromInventory;
    public event Action<GameObject> onWeaponSwitch;
    public event Action<InventorySlot> onRemoveInventoryUISlotAndSubscribers;


    public void AddItemToInventory(InventoryItemSO itemSO, int count, PickUpItem itemToDelete)
        => onItemAddedToInventory?.Invoke(itemSO, count, itemToDelete);
    public void RemoveFromInventory(string itemId, int count) => onRemoveFromInventory.Invoke(itemId, count);

    public void CloseInventoryPanel() => onInventoryPanelHide?.Invoke();
    public void ShowInventoryPanel() => onInventoryPanelShow?.Invoke();
    public void SwitchWeapon(GameObject weapon) => onWeaponSwitch?.Invoke(weapon);

    public void RemoveUISlotAndSubscribers(InventorySlot inventorySlot) => onRemoveInventoryUISlotAndSubscribers?.Invoke(inventorySlot);

}
