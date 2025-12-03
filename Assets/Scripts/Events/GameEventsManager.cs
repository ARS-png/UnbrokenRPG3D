using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    [HideInInspector] public QuestEvents questEvents;
    [HideInInspector] public DialogueEvents dialogueEvents;
    [HideInInspector] public CameraEvents cameraEvents;
    [HideInInspector] public TTSEvents tTSEvents;
    [HideInInspector] public AnimationEvents animationEvents;
    [HideInInspector] public QuestStepPrefabsEvents questStepPrefabsEvents;
    [HideInInspector] public InventoryEvents inventoryEvents;

    public static GameEventsManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            questEvents = new QuestEvents();
            dialogueEvents = new DialogueEvents();
            cameraEvents = new CameraEvents();
            tTSEvents = new TTSEvents();
            animationEvents = new AnimationEvents();
            questStepPrefabsEvents = new QuestStepPrefabsEvents();
            inventoryEvents = new InventoryEvents();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
