using TMPro;
using Unity.Collections;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    public static event System.Action OnDifficultyChanged;
    string[] difficultyLevels = new string[] { "Путник", "Испытание", "Дорога войны", "Легенда" };//

    [Header("Difficulty Settings")]
    public TMP_Dropdown difficultyDropdown;

 

    private void Start()
    {


        InitializDifficultyDropdown();

     

        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.OnUserLoggedIn += LoadCurrentDifficultySettings;
        }

        LoadCurrentDifficultySettings();

    }

    public int GetCountOfDifficultyLevels() // 
    { 
        return difficultyLevels.Length;
    }

    private void InitializDifficultyDropdown()
    {
       

        if (difficultyDropdown != null)
        {
            difficultyDropdown.ClearOptions();

            foreach (string difficultyName in difficultyLevels)
            {
                difficultyDropdown.options.Add(new TMP_Dropdown.OptionData(difficultyName));
            }

            difficultyDropdown.onValueChanged.AddListener(DiffifultyChanged);
        }
    }

    private void LoadCurrentDifficultySettings()
    {
   

        if (DatabaseManager.Instance != null && DatabaseManager.Instance.IsUserLoggedIn() && difficultyDropdown != null)
        {
            Settings userSettings = DatabaseManager.Instance.GetCurrentUserSettings();
            if (userSettings != null)
            {
                difficultyDropdown.SetValueWithoutNotify(userSettings.Difficulty);
            }

            DifficultySaver.Instance.SetDifficulty(userSettings.Difficulty);

            DifficultySaver.Instance.SetCountOfDifficultyLevels(GetCountOfDifficultyLevels());
        }
    }

    private void DiffifultyChanged(int selectedIndex)
    {
     

        if (DatabaseManager.Instance != null && DatabaseManager.Instance.IsUserLoggedIn())
        {
            Settings userSettings = DatabaseManager.Instance.GetCurrentUserSettings();
            if (userSettings != null)
            {
                userSettings.Difficulty = selectedIndex;
                bool success = DatabaseManager.Instance.UpdateCurrentUserSettings(userSettings);

                if (success)
                {

                    DifficultySaver.Instance.SetDifficulty(selectedIndex);
                    Debug.Log("difficulty on index" + selectedIndex + "save in SaverClass");
                }
            }
        }
    }

    private void OnDestroy()
    {


        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.OnUserLoggedIn -= LoadCurrentDifficultySettings;
        }
    }

}
