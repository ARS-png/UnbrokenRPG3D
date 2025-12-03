using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action onChangeInventory;
    public void ChangeInventory() => onChangeInventory?.Invoke(); 
}
