using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Ink.Runtime;

public class DialogueEvents
{
    public event Action<string> onEnterDialogue;
    public event Action onDialogueStarted;  
    public event Action OnDialogueFinished;
    public event Action<string, List<Choice>> onDisplayDialogue;
    public event Action<int> onUpdateChoiseIndex;
    public event Action<string, Ink.Runtime.Object> onUpdateInkVariable;

    public void EnterDialogue(string knotName) => onEnterDialogue?.Invoke(knotName);
    public void DialogueStarted() => onDialogueStarted?.Invoke();
    public void DialogueFinished() => OnDialogueFinished?.Invoke();
    public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices) => 
        onDisplayDialogue?.Invoke(dialogueLine, dialogueChoices);
    public void UpdateChoiseIndex(int choiseIndex) => onUpdateChoiseIndex?.Invoke(choiseIndex);
    public void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value) =>
        onUpdateInkVariable?.Invoke(name, value);
  

}
