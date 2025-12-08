using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField] private InventoryItemSO itemSO;

 
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
