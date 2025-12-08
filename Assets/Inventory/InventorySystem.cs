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
        if (itemToAdd == null)
        {
            Debug.LogError($"There is no scriptable object on item");
            return false;
        }

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


    public bool RemoveFromInventory(InventoryItemSO itemData, int amountToRemove)
    {
        if (FindFirstSlotWithSameData(itemData, out InventorySlot inventorySlot))
        {
            inventorySlot.RemoveFromStack(amountToRemove); //так же принудительно нужно кактто сохранить 
            //целостность item, потому что префаб оружия не получает события

            if (inventorySlot.Amount == 0)
            {
                GameEventsManager.instance.inventoryEvents.RemoveUISlotAndSubscribers(inventorySlot);//clear ui before deleted actualy value

                inventorySlot.ClearSlot();
            }
            return true;
        }
        return false;
    }


    public bool FindFirstSlotWithSameData(InventoryItemSO itemData, out InventorySlot possibleSlot)
    {
        possibleSlot = inventorySlots.FirstOrDefault(i => i.ItemSO == itemData);

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


    public InventorySlot GetSlotByItemId(string itemId)
    {
        return inventorySlots.FirstOrDefault(i => i.ItemSO != null && i.ItemSO.itemId == itemId);
    }


    public GameObject GetPrefabFromInventory(string itemId)
    {

        var inventorySlot = InventorySlots.FirstOrDefault(i => i.ItemSO != null && i.ItemSO.itemId == itemId);

        return inventorySlot?.ItemSO?.itemPrefab;
    }


    public InventoryItemSO.ItemType GetItemType(string itemId)
    {
        var inventorySlot = InventorySlots.FirstOrDefault(i => i.ItemSO.itemId == itemId);
        InventoryItemSO.ItemType itemType = inventorySlot.ItemSO.itemType;
        return itemType;
    }


}
