using UnityEngine;

public class MoveToPositionQuestStep : QuestStep
{
    private int countOfMovesToPosition = 0;

    private int movesToComplite = 5;

    private void Start()
    {
        GameEventsManager.instance.questStepPrefabsEvents.onPlayerDetected += DetectPlayer;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questStepPrefabsEvents.onPlayerDetected -= DetectPlayer;
    }

    private void DetectPlayer()
    {
        if (countOfMovesToPosition < movesToComplite)
        {
            countOfMovesToPosition++;
            UpdateState();
        }

        if (countOfMovesToPosition >= movesToComplite)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = countOfMovesToPosition.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.countOfMovesToPosition = System.Int32.Parse(state);
        UpdateState();
    }
}
