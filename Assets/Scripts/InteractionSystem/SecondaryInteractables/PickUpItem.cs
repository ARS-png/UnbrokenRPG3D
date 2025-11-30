using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : Interactable
{
    [Header("Item Data")]
    [SerializeField] string itemName;

    public override void Start()
    {
        base.Start();

        interactableName = itemName;
    }

    protected override void Interaction()
    {
        base.Interaction();
        print("I put " + itemName + "in my inventory");
        Destroy(this.gameObject);//уничтожается а не клладется в инвентарь
    }
}
