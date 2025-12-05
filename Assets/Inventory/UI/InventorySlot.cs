using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemSO itemSO;
    [SerializeField] private int stackSize;

    public InventoryItemSO ItemSO => itemSO;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemSO itemSO, int amount)
    {
        this.itemSO = itemSO;
        this.stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemSO = null;
        stackSize = -1;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemSO.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= ItemSO.MaxStackSize) return true;
        else return false;

    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public void UpdateInventorySlot(InventoryItemSO data, int amount)
    {
        itemSO = data;
        stackSize = amount;
    }


}
