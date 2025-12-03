using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemSO itemSO;
    [SerializeField] private int amount;

    public InventoryItemSO ItemSO => itemSO;

    public InventorySlot(InventoryItemSO itemSO, int amount)
    {
        this.itemSO = itemSO;
        this.amount = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemSO = null;
        amount = -1;
    }
}
