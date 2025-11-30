using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject registerMenu;
    public GameObject loginMenu;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject areYouSure;

    public Button newGameButton;
    public Button continueButton;

   


    private void Start()
    {
        DatabaseManager.Instance.OnUserLoggedIn += GoToMenu;

        newGameButton.onClick.AddListener(OnNewGameClicked);

        continueButton.onClick.AddListener(OnContinueButtonClicked);

        ShowCurrentMenu();

        
        


    }

    private void ShowCurrentMenu()
    {
    
        registerMenu.SetActive(false);
        loginMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        areYouSure.SetActive(false);

        
        if (DatabaseManager.Instance.IsUserLoggedIn())
        {
         
            GoToMenu();
        }
        else
        {
          
            GoToLoginMenu();
        }
    }

    public void GoToRegisterMenu()
    {
        registerMenu.SetActive(true);
    }

    public void GoToLoginMenu()
    {
        registerMenu.SetActive(false);

        registerMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void GoToMenu()
    {

        registerMenu.SetActive(false);
        loginMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GotoSettingMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void BackFromSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void ShowAreYouSure()
    {
        areYouSure.SetActive(true);
    }

    public void CloseAreYouSure()
    {
        areYouSure.SetActive(false);
    }

    private void OnNewGameClicked()
    {
        DatabaseManager.Instance.ResetCurrentUserGameSave();     
        SceneManager.LoadScene("SampleScene");    
    }

    private void OnContinueButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

