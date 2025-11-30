using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Start()
    {

        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.OnGamePaused += EnableCursor;
            PauseManager.Instance.OnGameResumed += DisableCursor;           
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

        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.OnGamePaused -= EnableCursor;
            PauseManager.Instance.OnGameResumed -= DisableCursor;
        }
    }
}