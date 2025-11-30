using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] SettingsApplier settingsApplier;

    private void Start()
    {
        DatabaseManager.Instance.OnUserLoggedIn += ApplyCurrentUserSettings;
        ApplyCurrentUserSettings();
    }
    public void ApplyCurrentUserSettings()
    {
        if (DatabaseManager.Instance == null)
        {
            Debug.LogError("🚨 DatabaseManager.Instance is NULL!");
            settingsApplier?.ApplyDefaultSettings();
            return;
        }
        if (!DatabaseManager.Instance.IsUserLoggedIn())
        {
            Debug.LogWarning("❌ No user logged in - applying default settings");

            settingsApplier.ApplyDefaultSettings();

            return;
        }

        Settings userSettings = DatabaseManager.Instance.GetCurrentUserSettings();
        if (userSettings != null)
        {
            settingsApplier.ApplySettings(userSettings);
            Debug.Log($"✅ Applied settings for user ID: {DatabaseManager.Instance.GetCurrentUserId()}");
            
        }
        else
        {
            Debug.LogError("❌ Failed to load user settings");

            settingsApplier.ApplyDefaultSettings();

        }
    }


    private void OnDestroy()
    {
        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.OnUserLoggedIn -= ApplyCurrentUserSettings;
        }
    }




}
