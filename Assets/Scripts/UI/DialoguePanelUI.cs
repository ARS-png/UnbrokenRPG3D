using Ink.Runtime;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject contentParent;

    [SerializeField] GameObject choicesPanel;

    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] DialogueChoiseButton[] choiseButtons;

    [Header("Text Position")]
    Vector2 choicesTextPosition = new Vector2(0, 120);//немного изменить названия
    Vector2 defaultTextPosition = new Vector2(0, 20);

    private void Awake()
    {
        contentParent.SetActive(false);
        choicesPanel.SetActive(false);
        ResetPanel();


    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.instance.dialogueEvents.OnDialogueFinished += DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        GameEventsManager.instance.dialogueEvents.OnDialogueFinished -= DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
    }

    private void DialogueStarted()
    {
        Cursor.visible = true;//
        Cursor.lockState = CursorLockMode.None;//изменить


        contentParent.SetActive(true);
        choicesPanel.SetActive(true);
    }

    private void DialogueFinished()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        contentParent.SetActive(false);
        choicesPanel.SetActive(false);

        ResetPanel();
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        dialogueText.text = dialogueLine;

        if (dialogueChoices.Count > choiseButtons.Length)
        {
            Debug.LogError($"More dialgue choices ({dialogueChoices.Count})" +
                $" than are supported ({choiseButtons.Length})");
        }

        foreach (DialogueChoiseButton choiseButton in choiseButtons)
        {
            choiseButton.gameObject.SetActive(false);
        }


        int choiseButtonIndex = dialogueChoices.Count - 1;

        if (dialogueChoices.Count == 0)
        {
            SetUIPosition(dialogueText, defaultTextPosition);
         
        }
        else
        {
            SetUIPosition(dialogueText, choicesTextPosition);
    
        }



        for (int inkChoiseIndex = 0; inkChoiseIndex < dialogueChoices.Count; inkChoiseIndex++)
        {
            Choice dialogueChoise = dialogueChoices[inkChoiseIndex];
            DialogueChoiseButton choiseButton = choiseButtons[choiseButtonIndex];

            choiseButton.gameObject.SetActive(true);
            choiseButton.SetChoiseText($"{inkChoiseIndex + 1}. " + dialogueChoise.text);
            choiseButton.SetChoiceIndex(inkChoiseIndex);

            if (inkChoiseIndex == 0)
            {
                choiseButton.SelectButton();
                GameEventsManager.instance.dialogueEvents.UpdateChoiseIndex(0);
            }

            choiseButtonIndex--;
        }
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }



    private void SetUIPosition(TextMeshProUGUI uiElement, Vector2 position)
    {
        RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = position;
        }
    }
}
