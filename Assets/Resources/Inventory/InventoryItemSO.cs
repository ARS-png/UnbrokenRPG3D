using UnityEngine;

[CreateAssetMenu(fileName = "BasicItemSO", menuName = "Inventory/New ItemSO", order = 1)]
public class InventoryItemSO : ScriptableObject
{
    [Header("General")]
    public int amount;
    public string itemType;
    public string itemId;


    [Header("UI")]
    [TextArea(4, 4)]
    public string description;
    public Sprite Icon;
}
