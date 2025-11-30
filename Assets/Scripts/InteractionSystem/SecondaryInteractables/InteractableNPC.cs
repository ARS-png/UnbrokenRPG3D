using UnityEngine;

public class InteractableNPC : Interactable
{
    protected Animator animator;
    private bool isHelloBefore = false;

    public override void Start()
    { 
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Interaction()
    {
        base.Interaction();

        if (!isHelloBefore)
        {
            //animator.SetTrigger("Wave");
            isHelloBefore = true;
        }
       

        Vector3 cameraPosition = Camera.main.transform.position;

        cameraPosition.y = this.transform.position.y;
        this.transform.LookAt(cameraPosition);

       

  
    }
}
