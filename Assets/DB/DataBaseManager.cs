using SQLite;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        InitializeDatabase();

        _userDbManager = new UserDbManager(_db, new SettingsDBManager(_db), new GameSaveDBManager(_db));
    }


    private SQLiteConnection _db;
    private string _dbPath;


    private UserDbManager _userDbManager;

    //[Header("Scene Manager")]
    //public _SceneManager sceneManager;

    public UIManager uIManager;


    public event System.Action OnUserLoggedIn;


    private void InitializeDatabase()
    {
        // База создается в папке проекта (рядом с Assets)
        string projectDbFolder = Path.Combine(Application.dataPath, "..", "Database");
        projectDbFolder = Path.GetFullPath(projectDbFolder); // Нормализуем путь

        // Создаем папку Database если её нет
        if (!Directory.Exists(projectDbFolder))
        {
            Directory.CreateDirectory(projectDbFolder);
            Debug.Log($"📁 Created folder: {projectDbFolder}");
        }

        _dbPath = Path.Combine(projectDbFolder, "users.db");
        Debug.Log($"📁 Database will be created at: {_dbPath}");

        _db = new SQLiteConnection(_dbPath);

        _db.CreateTable<Person>();
        _db.CreateTable<Settings>();
        _db.CreateTable<GameSave>();

        #region DebugCalls
        //Debug.Log("✅ Table 'Person' created successfully!");

        // Покажем где найти базу
        //Debug.Log($"🔍 Database location: {Path.GetDirectoryName(_dbPath)}");
        #endregion

    }

    public void ValidateLogin(string username, string password)
    {
        bool success = _userDbManager.ValidateLogin(username, password);

        if (success)
        {
            Debug.Log($" Welcome, {username}!");

            OnUserLoggedIn?.Invoke();     
        }

        else
        {
            Debug.LogError("Login failed!");
        }
    }

    public void Register(string username, string password)
    {
        bool success = _userDbManager.CreateUser(username, password);

        if (success)
        {
            Debug.Log($" Welcome(register), {username}!");
            OnUserLoggedIn?.Invoke();
    
        }
        else
        {
            Debug.LogError("Register failed!");
        }
    }


    public bool IsUserLoggedIn()
    {
        return _userDbManager.IsUserLoggedIn();
    }

    public Settings GetCurrentUserSettings()
    {
        return _userDbManager.GetCurrentUserSettings();
    }

    public GameSave GetCurrentUserGameSave()
    { 
        return _userDbManager.GetCurrentUserGameSave();
    }

    public bool UpdateCurrentUserSettings(Settings settings)
    {
        return _userDbManager.UpdateCurrentUserSettings(settings);
    }

    public bool UpdateCurrentUserGameSave(GameSave gameSave)
    {
        return _userDbManager.UpdateCurrentUserGameSave(gameSave);
    }

    public bool ResetCurrentUserGameSave()
    {
        return _userDbManager.ResetCurrentUserGameSave();
    }
        

    public int GetCurrentUserId()
    {
        return _userDbManager.GetCurrentUserId();
    }

    void OnDestroy()
    {
        _db?.Close();
    }
}