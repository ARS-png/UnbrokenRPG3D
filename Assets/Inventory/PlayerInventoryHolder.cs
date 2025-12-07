using UnityEngine;

public class PlayerInventoryHolder : InventoryHolder
{
    protected override void GetPrefabFromInventory(string itemId)
    {
        base.GetPrefabFromInventory(itemId);

        if (inventorySystem.GetPrefabFromInventory(itemId) == null) { return; }


        if (inventorySystem.GetItemType(itemId) == InventoryItemSO.ItemType.WEAPON)
        {
            GameObject weapon = inventorySystem.GetPrefabFromInventory(itemId);
            GameEventsManager.instance.inventoryEvents.SwitchWeapon(weapon);
        }
    }
}
