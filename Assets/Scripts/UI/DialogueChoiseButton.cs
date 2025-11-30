using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueChoiseButton : MonoBehaviour, ISelectHandler
{
    [Header("Components")]
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI choiseText;

    private int choiseIndex = -1;

    public void SetChoiseText(string choiseTextString)
    { 
        choiseText.text = choiseTextString; 
    }

    public void SetChoiceIndex(int choiseIndex)
    { 
        this.choiseIndex = choiseIndex;
    }

    public void SelectButton()
    { 
        button.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameEventsManager.instance.dialogueEvents.UpdateChoiseIndex(choiseIndex);
    }
}

