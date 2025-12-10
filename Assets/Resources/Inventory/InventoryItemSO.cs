using UnityEngine;

[CreateAssetMenu(fileName = "BasicItemSO", menuName = "Inventory/New ItemSO", order = 2)]
public class InventoryItemSO : ScriptableObject
{
    public enum ItemType
    {
        POTION,
        WEAPON
    }

    [Header("If The Weapon")]
    public InventoryWeaponItemSO WeaponItemSO;
    

    [Header("General")]
    public int amount;
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
