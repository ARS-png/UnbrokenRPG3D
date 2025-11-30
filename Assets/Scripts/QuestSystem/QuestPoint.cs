using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Dialogue (optional)")]
    [SerializeField] string dialogueKnotName;

    [Header("Quest Info")]
    [SerializeField] QuestInfoSO questInfoForPoint;


    [Header("Player Input")]
    [SerializeField] PlayerInput playerInput;


    [Header("Config")]
    [SerializeField] bool startPoint = true;
    [SerializeField] bool finishPoint = true;

    private InputAction interact;

    private bool playerIsNear = false;

    private string questId;
    private QuestState currentQuestState;

    private Color color;
    private void Awake()
    {
        questId = questInfoForPoint.QuestID;

        interact = playerInput.actions["Interact"];

        color = Color.yellow;
    }

    private void OnEnable()
    {
        StartCoroutine(SubscribeAfterComponentInitialize());

        interact.performed += SubmitPressed;
    }


    private IEnumerator SubscribeAfterComponentInitialize()
    {
        while (GameEventsManager.instance == null)
        {
            yield return null;
        }

        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }



    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;

        interact.performed -= SubmitPressed;
    }


    private void SubmitPressed(InputAction.CallbackContext context)
    {
        if (!playerIsNear)
        {
            return;
        }

        if (!dialogueKnotName.Equals(""))
        {
            GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        }

        else //до сюда ниеогдп не доходит
        {
            if (currentQuestState == QuestState.CAN_START && startPoint)
            {
                GameEventsManager.instance.questEvents.StartQuest(questId);
            }

            else if (currentQuestState == QuestState.CAN_FINISH && finishPoint)
            {
                GameEventsManager.instance.questEvents.FinishQuest(questId);
            }
        }
    }

    private void QuestStateChange(Quest quest)  //тоже вроде как больше не нужно
    {
        if (quest.info.QuestID == questId)
        {
            currentQuestState = quest.state;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;

            color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }

        color = Color.yellow;
    }

    private void OnDrawGizmos()
    {
        var radius = GetComponent<SphereCollider>().radius;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
