using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Start()
    {

        GameEventsManager.instance.inventoryEvents.onInventoryPanelShow += EnableCursor;

        GameEventsManager.instance.inventoryEvents.onInventoryPanelHide += DisableCursor;


        if (GameStopManager.Instance != null)
        {
            GameStopManager.Instance.OnGamePaused += EnableCursor;
            GameStopManager.Instance.OnGameResumed += DisableCursor;
        }
        else
        {
            Debug.LogError("PauseManager.Instance is null!");
        }

        HealthSystem.OnPlayerDie += EnableCursor; //
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Debug.Log("Cursor enabled - visible and unlocked");

    }

    public void DisableCursor()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Debug.Log("Cursor disabled - hidden and locked");
    }

    private void OnDestroy()
    {

        if (GameStopManager.Instance != null)
        {
            GameStopManager.Instance.OnGamePaused -= EnableCursor;
            GameStopManager.Instance.OnGameResumed -= DisableCursor;
        }

        GameEventsManager.instance.inventoryEvents.onInventoryPanelShow -= EnableCursor;
        GameEventsManager.instance.inventoryEvents.onInventoryPanelHide -= DisableCursor;
    }
}