using Ink.Parsed;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public int InventorySize() => inventorySlots.Count;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemSO itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlot))
        {
            foreach (var slot in inventorySlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    return true;
                }
            }

        }

        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemSO itemToAdd, out List<InventorySlot> inventorySlot)
    {
        inventorySlot = inventorySlots.Where(i => i.ItemSO == itemToAdd).ToList();

        return inventorySlot == null ? false : true;

    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemSO == null);
        return freeSlot == null ? false : true; 
    }

}
