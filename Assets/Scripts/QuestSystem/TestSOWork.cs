using UnityEngine;

public class TestSOWork : MonoBehaviour
{
    private void Start()
    {
        var allQuestsInfo = Resources.LoadAll<QuestInfoSO>("");

        if (allQuestsInfo.Length == 0)
        {
            Debug.LogError("There is no Chest assts");
        }

        foreach (var questInfo in allQuestsInfo) 
        {
            Debug.Log(questInfo.QuestName);
        }
    }
}
