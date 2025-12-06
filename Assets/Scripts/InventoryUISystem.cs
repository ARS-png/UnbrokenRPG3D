using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUISystem : MonoBehaviour
{
    private List<InventorySlotUI> itemUIButtonsList = new List<InventorySlotUI>();

    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<InventorySlotUI>())
        {
            itemUIButtonsList.Add(button);
        }
    }

    public void UpdateUIButtonItem(InventorySlot invSLot)
    {
        if (FindFirstButton(invSLot, out InventorySlotUI slotUI))
        {
            slotUI.SetItem(invSLot.ItemSO.itemId, invSLot.ItemSO.Icon, invSLot.Amount);
        }

        else if (HasFreeButton(out InventorySlotUI freeSlotUI))
        {
            freeSlotUI.SetItem(invSLot.ItemSO.itemId, invSLot.ItemSO.Icon, invSLot.Amount);
        }
    }

    private bool FindFirstButton(InventorySlot invSLot, out InventorySlotUI freeSlotUIButton)
    {
        freeSlotUIButton = itemUIButtonsList.FirstOrDefault(i => i.CurrentItemId == invSLot.ItemSO.itemId);

        return freeSlotUIButton == null ? false : true;
    }

    public bool HasFreeButton(out InventorySlotUI freeSlotUIButton)
    {
        freeSlotUIButton = itemUIButtonsList.FirstOrDefault(i => i.CurrentItemId == "");
        return freeSlotUIButton != null;
    }


}

