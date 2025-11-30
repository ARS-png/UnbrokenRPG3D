using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BasicQuestSO", menuName = "Quests/New QuestSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    public string QuestID;

    [Header("General")]
    public string QuestName;
    public string QuestDescription;

    [Header("Requirements")]
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int goldReward;

    public int experienceReward;

    

    private void OnValidate()
    {
#if UNITY_EDITOR
        QuestID = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
