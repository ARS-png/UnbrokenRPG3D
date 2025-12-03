using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlotContainer 
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public int InventorySize() => inventorySlots.Count;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public InventorySlotContainer(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++) 
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemSO item, int amount)
    {
        inventorySlots[0] = new InventorySlot(item, amount);
        return true;
    }

}
