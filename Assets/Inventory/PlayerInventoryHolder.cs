using UnityEngine;

public class PlayerInventoryHolder : InventoryHolder
{
    protected override void RemoveFromInventory(string itemId, int amountToRemove)
    {
        if (inventorySystem.GetPrefabFromInventory(itemId) == null) { return; }
        var itemType = inventorySystem.GetItemType(itemId);

        switch (itemType)
        {
            case (InventoryItemSO.ItemType.WEAPON):
                GameObject weapon = inventorySystem.GetPrefabFromInventory(itemId);
                GameEventsManager.instance.inventoryEvents.SwitchWeapon(weapon);
                break;

            default: break;
        }

        base.RemoveFromInventory(itemId, amountToRemove);     

    }
}
