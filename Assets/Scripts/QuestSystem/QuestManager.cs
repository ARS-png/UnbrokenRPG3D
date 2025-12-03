using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;

    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
        }

        StartCoroutine(SendInitialStatesAfterDelay()); //change later
    }

    private IEnumerator SendInitialStatesAfterDelay()
    {
        yield return new WaitForEndOfFrame();
        foreach (Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState state)//local
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);//подпимеп в QiestPoint
    }

    private QuestState GetQuestState(string id)
    {
        Quest quest = GetQuestById(id);
        return quest.state;
    }

    private void StartQuest(string id)
    {
        if (GetQuestState(id) != QuestState.CAN_START)
        {
            Debug.LogWarning($"Quest with ID {id} is already started!");
            return;
        }

        Quest quest = GetQuestById(id);

        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.QuestID, QuestState.IN_PROGRESS); 


        Debug.Log($"Quest With id: {id} is Started");

        Debug.Log($"Quest with id {id} quest state change to {quest.state}");
    }

    private void AdvanceQuest(string id)
    {
        Debug.Log($"Quest is Advanced with id: {id}");

        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();

        if (quest.CurrentQuestExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.QuestID, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        if (GetQuestState(id) != QuestState.CAN_FINISH)
        {
            Debug.LogWarning($"Quest with id: {id} can not be finished, ir's state is: {GetQuestState(id)}");
            return;
        }

        Debug.Log($"Quest is Finished with id: {id}");

        Quest quest = GetQuestById(id);
        ChangeQuestState(quest.info.QuestID, QuestState.FINISHED);
    }


    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.QuestID))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map");
            }

            idToQuestMap.Add(questInfo.QuestID, LoadQuest(questInfo));
        }

        return idToQuestMap;

    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];

        if (quest == null)
        {
            Debug.LogError($"ID not found in questMap + {id}");
        }

        return quest;
    }


    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }


    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();

            string serializedData = JsonUtility.ToJson(questData);

            PlayerPrefs.SetString(quest.info.QuestID, serializedData);

            Debug.Log(serializedData);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save quest with id " + quest.info.QuestID + ":" + e);
        }
    }


    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;

        try
        {
            if (PlayerPrefs.HasKey(questInfo.QuestID) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.QuestID);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);

      //добавить сохранини в QuestPoinit
              /*  GameEventsManager.instance.questEvents.QuestStateChange(quest);*///

                //Debug.Log("asufhasdkjlfhaslkjdfhaslkjdfh" + serializedData);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {

            Debug.LogError("Failed to load quest with id " + quest.info.QuestID + ":" + e);
        }

        return quest;
    }
}
