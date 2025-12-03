using UnityEngine;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    [SerializeField] protected InventorySlotContainer slotContainer;

    public InventorySlotContainer SlotContainer=> slotContainer;

    private void Awake()
    {
        slotContainer = new InventorySlotContainer(inventorySize);
    }
}
