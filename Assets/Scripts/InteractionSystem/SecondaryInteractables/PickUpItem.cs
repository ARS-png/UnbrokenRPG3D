using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PickUpItem : Interactable
{
    [SerializeField] private InventoryItemSO itemSO;

    public InventoryItemSO GetItemSO() => itemSO;

   
 
    public override void Start()
    {
        base.Start();
        
    }

    protected override void Interaction()
    {
        base.Interaction();
        //print("I put " + interactableName + "in my inventory");

        
        GameEventsManager.instance.inventoryEvents.AddItemToInventory(itemSO, 1, this);
    }
}
