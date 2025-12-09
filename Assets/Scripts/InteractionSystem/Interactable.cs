using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Data")]
    public string interactableName = "";
    public float interactionDistance = 2;
    [SerializeField] bool isInteractable = true;

    InteractableNameText interactableNameText;
    GameObject interactableNameCanvas;

    public virtual void Start()
    {
        interactableNameCanvas = GameObject.FindGameObjectWithTag("Canvas");

        if (interactableNameCanvas != null)
        {
            interactableNameText = interactableNameCanvas.GetComponentInChildren<InteractableNameText>();


            if (interactableNameText == null)
            {
                Debug.LogError($"❌ InteractableNameText not found in Canvas for {gameObject.name}");
            }
        }
        else
        {
            Debug.LogError($"❌ Canvas with tag 'Canvas' not found for {gameObject.name}");
        }


    }

    public void SetIsInteractableValue(bool value)
    {
        isInteractable = value;
    }

    public void TargetOn()
    {
        if (!isInteractable) { return; }

        if (interactableNameText != null)
        {
            interactableNameText.ShowText(this);
            interactableNameText.SetInteractableNamePosition(this);
        }

    }

    public void TargetOff()
    {
        if (!isInteractable) { return; }
        if (interactableNameText != null)
        {
            interactableNameText.HideText();
        }
    }

    public void Interact()
    {
        if (isInteractable) Interaction();
    }

    protected virtual void Interaction()
    {
        //print("interact with: " + this.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
    private void OnDestroy()
    {
        TargetOff();
    }
}
