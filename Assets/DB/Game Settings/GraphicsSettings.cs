using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    [Header("QUALITY SETTINGS")]
    public TMP_Dropdown qualityDropdown;

 

    void Start()
    {
    
        InitializeQualityDropdown();

        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.OnUserLoggedIn += LoadCurrentQualitySettings;
        }

        LoadCurrentQualitySettings();
    }

    private void InitializeQualityDropdown()
    {

        if (qualityDropdown != null)
        {
            qualityDropdown.ClearOptions();

            foreach (string qualityName in QualitySettings.names)
            {
                qualityDropdown.options.Add(new TMP_Dropdown.OptionData(qualityName));
            }

            qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        }
    }

    private void OnQualityChanged(int selectedIndex)
    {
        if (DatabaseManager.Instance != null && DatabaseManager.Instance.IsUserLoggedIn())
        {
            Settings userSettings = DatabaseManager.Instance.GetCurrentUserSettings();
            if (userSettings != null)
            {
                userSettings.QualityLevel = selectedIndex;
                bool success = DatabaseManager.Instance.UpdateCurrentUserSettings(userSettings);

                if (success)
                {
                    QualitySettings.SetQualityLevel(selectedIndex);
                    Debug.Log($"✅ Quality saved: {QualitySettings.names[selectedIndex]}");
                }
            }
        }
    }

    private void LoadCurrentQualitySettings()
    {
        if (DatabaseManager.Instance != null && DatabaseManager.Instance.IsUserLoggedIn() && qualityDropdown != null)
        {
            Settings userSettings = DatabaseManager.Instance.GetCurrentUserSettings();
            if (userSettings != null)
            {
                qualityDropdown.SetValueWithoutNotify(userSettings.QualityLevel);
            }


        }


    }

    private void OnDestroy()
    {

        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.OnUserLoggedIn -= LoadCurrentQualitySettings;
        }
    }
}