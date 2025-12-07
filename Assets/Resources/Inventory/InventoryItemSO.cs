using UnityEngine;

[CreateAssetMenu(fileName = "BasicItemSO", menuName = "Inventory/New ItemSO", order = 1)]
public class InventoryItemSO : ScriptableObject
{
    public enum ItemType
    {
        POTION,
        WEAPON
    }


    [Header("General")]
    public int amount;
    //public string itemType;
    public string itemId;
    public ItemType itemType;
   

    [Header("UI")]
    [TextArea(4, 4)]
    public string description;
    public Sprite Icon;

    [Space]
    [Header("Prefab")]
    public GameObject itemPrefab;
}
