using UnityEngine;
using UnityEngine.UIElements;
[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemSO itemSO;
    [SerializeField] private int amount;

    public InventoryItemSO ItemSO => itemSO;
    public int Amount => amount;

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot() //почему картинка остается?
    {
        itemSO = null;
        amount = -1;
    }

    public void AddToStack(int amount)
    {
        this.amount += amount;
    }

    public void RemoveFromStack(int _amount)
    {
        this.amount -= _amount;
    }

    public void UpdateInventorySlot(InventoryItemSO data, int amount)
    {
        itemSO = data;
        this.amount = amount;
    }


}
