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

    private readonly int amountToDeleteWhenClickOnItem = 1;

    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<InventorySlotUI>(includeInactive: true))
        {
            itemUIButtonsList.Add(button);
        }

        Debug.Log("LIST SIZE IS: " + itemUIButtonsList.Count);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onRemoveInventoryUISlotAndSubscribers += RemoveUISlotAndSubscribers;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onRemoveInventoryUISlotAndSubscribers -= RemoveUISlotAndSubscribers;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void UpdateUIButtonItem(InventorySlot invSLot)
    {
        Debug.Log("UI is updated");
        if (invSLot.ItemSO == null)//
        {

            Debug.Log("inventory slot ins null");
            
            return;
        }

        if (FindFirstButton(invSLot, out InventorySlotUI slotUI))
        {
            slotUI.SetItem(invSLot.ItemSO.itemId, invSLot.ItemSO.Icon, invSLot.Amount);

            slotUI.button.onClick.RemoveAllListeners();
            slotUI.button.onClick.AddListener(() => RemoveFromInventory(slotUI));
        }

        else if (HasFreeButton(out InventorySlotUI freeSlotUI))
        {
            freeSlotUI.SetItem(invSLot.ItemSO.itemId, invSLot.ItemSO.Icon, invSLot.Amount);

            freeSlotUI.button.onClick.RemoveAllListeners();
            freeSlotUI.button.onClick.AddListener(() => RemoveFromInventory(freeSlotUI));
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

    private void RemoveFromInventory(InventorySlotUI slotUI)
    {
        GameEventsManager.instance.inventoryEvents.RemoveFromInventory(slotUI.CurrentItemId, amountToDeleteWhenClickOnItem);
    }

    private void RemoveUISlotAndSubscribers(InventorySlot inventorySlot)
    {
        if (FindFirstButton(inventorySlot, out InventorySlotUI inventorySlotUI))
        {
            inventorySlotUI.ResetSlot();
            inventorySlotUI.button.onClick.RemoveAllListeners();
        }
    }
}

