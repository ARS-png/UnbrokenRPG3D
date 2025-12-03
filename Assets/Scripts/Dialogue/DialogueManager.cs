using Ink.Runtime;

using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class DialogueManager : MonoBehaviour
{
    [Header("Player Flags")]
    [SerializeField] bool isSpeakEnable;

    [Header("Player Input")]
    [SerializeField] PlayerInput playerInput;
    private InputAction interact;

    [Header("Ink Story")]
    [SerializeField] TextAsset inkJson;

    Story story;

    private int currentChoiseIndex = -1;

    private bool dialoguePlaying = false;

    private InkExternalFunctions inkExternalFunctions;

    private InkDialogueVariables inkDialogueVariables;




    private const string SPEAKER_TAG = "speaker";
    private const string ANIM_TAG = "anim";

    private string currentSpeaker;

    private void Awake()
    {
        interact = playerInput.actions["Interact"];

        story = new Story(inkJson.text);

        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
        inkDialogueVariables = new InkDialogueVariables(story);    
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        interact.performed += SubmitPressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiseIndex += UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onUpdateInkVariable += UpdateInkDialogueVariable;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        interact.performed -= SubmitPressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiseIndex -= UpdateChoiceIndex;
        GameEventsManager.instance.dialogueEvents.onUpdateInkVariable -= UpdateInkDialogueVariable;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest) //когда изменяется состояние квеста 
    {
        GameEventsManager.instance.dialogueEvents.UpdateInkDialogueVariable(quest.info.QuestID + "State",
            new StringValue(quest.state.ToString())
            );
    }

    private void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value)
    {
        inkDialogueVariables.UpdateVariableState(name, value);
    }

    private void UpdateChoiceIndex(int choiseIndex)
    {
        this.currentChoiseIndex = choiseIndex;
    }


    private void SubmitPressed(InputAction.CallbackContext callbackContext)
    {
        if (!dialoguePlaying) //Enter один раз
        {
            return;
        }

        ContinueOrExitDialogue();
    }

    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying)
        {
            return;
        }

        dialoguePlaying = true;


        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);

            GameEventsManager.instance.dialogueEvents.DialogueStarted();
            GameEventsManager.instance.cameraEvents.ChangeCamera("DialogueCam");
        }
        else
        {
            Debug.LogWarning("Knot name was the empty string when enter the dialogue");
        }

        inkDialogueVariables.SyncVariablesAndStartListening(story);
    }

    private void ContinueOrExitDialogue()
    {
        //make a choise, if applicable

        if (story.currentChoices.Count > 0 && currentChoiseIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiseIndex);
            //reset choise index for next time

            currentChoiseIndex = -1;
        }

        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            ParseAnimationTags();

            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }

            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            {
                GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);

                if (isSpeakEnable)
                {
                    GameEventsManager.instance.tTSEvents.Speak(dialogueLine);
                }
            }
        }

        else if (story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        dialoguePlaying = false;



        GameEventsManager.instance.dialogueEvents.DialogueFinished();
        GameEventsManager.instance.cameraEvents.ChangeCamera("ThirdPersonCam");

        inkDialogueVariables.StopListening(story);

        story.ResetState();
    }


    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }

    private void ParseAnimationTags()
    {
        List<string> allTags = story.currentTags;

        if (allTags.Count > 0)
        {
            foreach (string tag in allTags)
            {
                string[] splitingTag = tag.Split(":");
                if (splitingTag.Length != 2)
                {
                    Debug.LogError("Tag could not parsed " + tag);
                }
                string tagKey = splitingTag[0].Trim();
                string tagValue = splitingTag[1].Trim();

                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        Debug.Log("speaker = " + tagValue);
                        currentSpeaker = tagValue;
                        break;
                    case ANIM_TAG:
                        Debug.Log("anim = " + tagValue);
                        GameEventsManager.instance.animationEvents.PlayAnimation(currentSpeaker, tagValue);
                        break;
                    default:
                        Debug.LogWarning("Tag cane but it is not correctly being handled" + tag);
                        break;
                }
            }
        }
    }
}
