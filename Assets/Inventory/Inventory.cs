using Ink.Parsed;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour //вроде как вообще не нужен
{
    private Dictionary<string, Item> itemMap;

    private Dictionary<string, Item> actualItemMap;

    private void Awake()
    {
        itemMap = CreateItemMap();
        actualItemMap = new Dictionary<string, Item>();

        AddItem("this");
        Debug.Log("Count of items in actual inventory is: " + actualItemMap.Count);

        GameEventsManager.instance.inventoryEvents.ChangeInventory();//custom
    }

    private Dictionary<string, Item> CreateItemMap()
    {
        InventoryItemSO[] allItems = Resources.LoadAll<InventoryItemSO>("Inventory");

        Dictionary<string, Item> idToItemMap = new Dictionary<string, Item>();

        foreach (InventoryItemSO itemInfo in allItems)
        {
            if (idToItemMap.ContainsKey(itemInfo.itemId))
            {
                Debug.LogWarning("Duplicate ID found when creating item map");
            }

            idToItemMap.Add(itemInfo.itemId, new Item(itemInfo));
        }

        return idToItemMap;
    }

    private void AddItem(string id)
    {
        if (actualItemMap.ContainsKey(id))
        {
            Debug.LogWarning($"Item {id} is already in the inventory. Cannot add duplicate.");
            return;
        }


        Item item = GetItemById(id);

        if (item == null)
        {
            Debug.LogWarning($"Item {id} does not exist in possible items of the inventory");
            return;
        }

        Debug.Log($"Item {id} added to the inventory");

        actualItemMap.Add(id, item);
    }

    private Item GetItemById(string id)
    {
        Item item;
        bool result = itemMap.TryGetValue(id, out item);

        if (!result)
        {
            Debug.LogWarning($"{id} does not exist in itemMap");
            return null;
        }
        return item;
    }

    public List<Item> GetItemsList() => itemMap.Values.ToList<Item>();

}
