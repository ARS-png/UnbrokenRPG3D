using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [Header("Login UI Elements")]
    public Button loginButton;
    public TMP_InputField loginInputUsername;
    public TMP_InputField loginInputPassword;

    [SerializeField] DatabaseManager databaseManager;


    private void Start()
    {
        if (loginButton != null)
        {
            loginButton.onClick.AddListener(OnLoginButtonClick);
        }
        else
        {
            Debug.LogError("Login button not assigned!");
        }
    }

    public void OnLoginButtonClick()
    {
        string username = loginInputUsername.text;
        string password = loginInputPassword.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Username or password is empty!");
            return;
        }

        DatabaseManager.Instance.ValidateLogin(username, password);    


    }


}
