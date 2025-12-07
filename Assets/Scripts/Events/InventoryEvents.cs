using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<InventoryItemSO, int, PickUpItem> onItemAddedToInventory;

    public event Action onInventoryPanelHide;
    public event Action onInventoryPanelShow;
    public event Action<string> onGetPrefabFromInventory;
    public event Action<GameObject> onWeaponSwitch;

    public void AddItemToInventory(InventoryItemSO itemSO, int count, PickUpItem itemToDelete) 
        => onItemAddedToInventory?.Invoke(itemSO, count, itemToDelete);
    public void CloseInventoryPanel() => onInventoryPanelHide?.Invoke();
    public void ShowInventoryPanel() => onInventoryPanelShow?.Invoke();
    public void GetPrefabFromInventory(string itemId) => onGetPrefabFromInventory.Invoke(itemId);
    public void SwitchWeapon(GameObject weapon) => onWeaponSwitch?.Invoke(weapon);
}
