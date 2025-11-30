using UnityEngine;

public class FindSomeOneQuestStep : QuestStep
{
    private bool isFind = false;

    private void OnEnable()
    {
        GameEventsManager.instance.questStepPrefabsEvents.onFindSomeOne += Find;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questStepPrefabsEvents.onFindSomeOne -= Find;
    }

    protected override void SetQuestStepState(string state)
    {
        bool result = bool.TryParse(state, out this.isFind);
        if (!result)
        {
            Debug.LogError($"State: {state} can not set bool variable");
            return;
        }
        UpdateState();
    }

    private void UpdateState()
    {
        string state = isFind.ToString();
        ChangeState(state);
    }

    private void Find() // if find someone
    {
        FinishQuestStep();
    }
}
