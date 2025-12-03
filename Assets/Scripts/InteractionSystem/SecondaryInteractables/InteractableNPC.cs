using UnityEngine;

public class InteractableNPC : Interactable
{
    protected Animator animator;
    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }


    protected override void Interaction()
    {
        base.Interaction();

        Vector3 cameraPosition = Camera.main.transform.position;

        cameraPosition.y = this.transform.position.y;
        this.transform.LookAt(cameraPosition);

        GameEventsManager.instance.questStepPrefabsEvents.FindSomeOne(interactableName);
        Debug.Log(interactableName);
    }
}
