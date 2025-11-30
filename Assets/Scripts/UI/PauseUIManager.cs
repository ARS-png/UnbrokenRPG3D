using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseBackground;

    [SerializeField] Button saveButton;

    private void Start()
    {
        PauseManager.Instance.OnGamePaused += EnableBackgroundPause;
        PauseManager.Instance.OnGameResumed += DisableBackgroundPause;

        saveButton.onClick.AddListener(SaveGameData);
    }

    public void EnableBackgroundPause()
    {
        pauseBackground.SetActive(true);
        
    }

    public void DisableBackgroundPause()
    {
        pauseBackground.SetActive(false);
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
  
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.OnGamePaused -= EnableBackgroundPause;
        }
    }

}
