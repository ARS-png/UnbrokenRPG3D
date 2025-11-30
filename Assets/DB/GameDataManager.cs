using UnityEditor.Rendering;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    [SerializeField] private Transform player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveCurrentPlayerPosition();
    }

    public void SaveCurrentPlayerPosition()
    {
        if (DatabaseManager.Instance == null)
        {
            Debug.LogWarning("❌ DatabaseManager instance not found!");
            return;
        }

        if (!DatabaseManager.Instance.IsUserLoggedIn())
        {
            Debug.LogWarning("⚠️ User not logged in. Cannot save position.");
            return;
        }

        GameSave userGameSave = DatabaseManager.Instance.GetCurrentUserGameSave();
        if (userGameSave == null)
        {
            Debug.LogError("❌ Failed to get user gameSave!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("❌ User Reference Is Null!");
            return;
        }

        userGameSave.XPosition = (int)player.position.x; 
        userGameSave.YPosition = (int)player.position.y;
        userGameSave.ZPosition = (int)player.position.z;

        bool success = DatabaseManager.Instance.UpdateCurrentUserGameSave(userGameSave);

        if (success)
        {
            Debug.Log($"✅ Player position saved to database: {player.position}");
        }
        else
        {
            Debug.LogError("❌ Failed to save player position to database!");
        }
    }
    public void LoadPlayerPosition()
    {
        try
        {
            if (DatabaseManager.Instance == null || !DatabaseManager.Instance.IsUserLoggedIn())
            {
                Debug.LogWarning("⚠️ Cannot load position - not initialized");
                return;
            }

            GameSave userGameSave = DatabaseManager.Instance.GetCurrentUserGameSave();
            if (userGameSave == null)
            {
                Debug.Log("ℹ️ No saved game data found");
                return;
            }


            if (userGameSave.XPosition != 0 || userGameSave.YPosition != 0 || userGameSave.ZPosition != 0)
            {
                Vector3 savedPosition = new Vector3(
                    userGameSave.XPosition,
                    userGameSave.YPosition,
                    userGameSave.ZPosition
                );

                player.position = savedPosition;
                Debug.Log($"✅ Player position loaded: {savedPosition}");
            }
            else
            {
                Debug.Log("ℹ️ Default position detected, not loading");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Error loading player position: {e.Message}");
        }
    }


}
