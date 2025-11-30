//using UnityEngine;

//public class InteractableLoot : Interactable
//{
//    public override void Start()
//    {
//        base.Start();
//    }

//    protected override void Interaction()
//    {
//        base.Interaction();

//        print("Unfoternutely you can't take my loot yet");
//        DisableCollider();
//        Destroy(this);
//    }

//    void DisableCollider()
//    {
//        if (TryGetComponent(out BoxCollider boxCollider))
//        {
//            boxCollider.enabled = false;
//        }
//        else if (TryGetComponent(out CapsuleCollider capsuleCollider))
//        {
//            capsuleCollider.enabled = false;
//        }
//        else
//        {
//            print("error, no collider found");
//        }
//    }

//}
