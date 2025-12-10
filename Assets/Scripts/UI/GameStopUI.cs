using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameStopUI : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button saveButton;

    [Header("Inventory")]
    [SerializeField] GameObject inventoryPanel;


    private void Start()
    {
        GameStopManager.Instance.OnGamePaused += EnableBackgroundPause;
        GameStopManager.Instance.OnGameResumed += DisableBackgroundPause;

        GameEventsManager.instance.inventoryEvents.onInventoryPanelShow += ShowInventoryPanel;
        GameEventsManager.instance.inventoryEvents.onInventoryPanelHide += HideInventoryPanel;


        saveButton.onClick.AddListener(SaveGameData);
    }

    public void EnableBackgroundPause()
    {
        pausePanel.SetActive(true);
    }

    public void DisableBackgroundPause()
    {
        pausePanel.SetActive(false);
    }

    public void ShowInventoryPanel()
    {
        inventoryPanel.SetActive(true);
    }

    public void HideInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }


    private void SaveGameData()
    {
        Debug.Log("🔄 Save button clicked!");
        if (GameDataManager.Instance == null)
        {
            Debug.Log("GameDataManager is null");
        }
        GameDataManager.Instance.SaveCurrentPlayerPosition();
    }


    private void OnDestroy()
    {

        if (GameStopManager.Instance != null)
        {
            GameStopManager.Instance.OnGamePaused -= EnableBackgroundPause;
            GameStopManager.Instance.OnGameResumed -= DisableBackgroundPause;
        }

        GameEventsManager.instance.inventoryEvents.onInventoryPanelShow -= ShowInventoryPanel;
        GameEventsManager.instance.inventoryEvents.onInventoryPanelHide -= HideInventoryPanel;
    }
}
