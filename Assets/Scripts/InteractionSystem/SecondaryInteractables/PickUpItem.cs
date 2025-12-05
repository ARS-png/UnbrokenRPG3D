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
        print("I put " + interactableName + "in my inventory");

        var inventoryHolder = this.transform.GetComponent<InventoryHolder>();

        if (inventoryHolder == null)
        {
            Debug.LogError("");
            return;
        }
        else
        {
            if (inventoryHolder.InventorySystem.AddToInventory(itemSO, 1))
            {
                Destroy(this.gameObject);
            }
        }
          
    }
}
