using UnityEngine;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    [SerializeField] protected InventorySlotContainer slotContainer;

    public InventorySlotContainer InventorySystem=> slotContainer;

    private void Awake()
    {
        slotContainer = new InventorySlotContainer(inventorySize);
    }
}
