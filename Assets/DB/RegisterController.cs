using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    [Header("REGISER UI Elements")]
    public Button registerButton;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField passwordRepeatInput;

    private void Start()
    {
        if (registerButton != null)
        {
            registerButton.onClick.AddListener(OnRegisterButtonClick);
        }
        else
        {
            Debug.LogError("Register button not assigned!");

        }
    }
    

    public void OnRegisterButtonClick()
    {

        if (usernameInput == null || passwordInput == null)
        {
            Debug.LogError("UI elements not assigned!");
            return;
        }

        string username = usernameInput.text;
        string password = passwordInput.text;
        string passwordRepeat = passwordRepeatInput.text;

        if (username == "" || password == "")
        {
            Debug.LogError("UI elements are pustio!");
            return;
        }

        if (password != passwordRepeat) //
        {
            Debug.Log("Пароли не совпадают!");
            return;
        }

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return;
        }

        DatabaseManager.Instance.Register(username, password);

    }
}
