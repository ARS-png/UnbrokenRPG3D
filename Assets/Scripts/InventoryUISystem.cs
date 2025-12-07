using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
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

            slotUI.button.onClick.RemoveAllListeners();
            slotUI.button.onClick.AddListener(() => GetPrefabFromInventory(slotUI));
        }

        else if (HasFreeButton(out InventorySlotUI freeSlotUI))
        {
            freeSlotUI.SetItem(invSLot.ItemSO.itemId, invSLot.ItemSO.Icon, invSLot.Amount);

            freeSlotUI.button.onClick.RemoveAllListeners();
            freeSlotUI.button.onClick.AddListener(() => GetPrefabFromInventory(freeSlotUI));
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


    private void GetPrefabFromInventory(InventorySlotUI slotUI)
    {
        GameEventsManager.instance.inventoryEvents.GetPrefabFromInventory(slotUI.CurrentItemId);
    }




}

