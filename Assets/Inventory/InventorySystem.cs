using Ink.Parsed;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        if (FindFirstSlotWithSameData(itemToAdd, out InventorySlot inventorySlot))
        {
            inventorySlot.AddToStack(amountToAdd);
            return true;
        }


        else if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            return true;
        }

        return false;
    }


    public bool FindFirstSlotWithSameData(InventoryItemSO itemToAdd, out InventorySlot possibleSlot)
    {
        possibleSlot = inventorySlots.FirstOrDefault(i => i.ItemSO == itemToAdd);

        return possibleSlot == null ? false : true;

    }


    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemSO == null);
        return freeSlot == null ? false : true;
    }


    public InventorySlot GetSlotByData(InventoryItemSO itemSO)
    {
        return inventorySlots.FirstOrDefault(i => i.ItemSO == itemSO);
    }


    public GameObject GetPrefabFromInventory(string itemId)
    {
        var inventorySlot = InventorySlots.FirstOrDefault(i => i.ItemSO.itemId == itemId);

        return inventorySlot.ItemSO.itemPrefab;
    }

    public InventoryItemSO.ItemType GetItemType(string itemId)
    {
        var inventorySlot = InventorySlots.FirstOrDefault(i => i.ItemSO.itemId == itemId);
        InventoryItemSO.ItemType itemType = inventorySlot.ItemSO.itemType;
        return itemType;
    }
}
