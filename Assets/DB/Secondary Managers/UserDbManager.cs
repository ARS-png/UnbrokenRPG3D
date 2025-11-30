using UnityEngine;
using System;
using System.Linq;
using SQLite;
using System.Runtime.InteropServices.WindowsRuntime;

public class UserDbManager
{
    private readonly SQLiteConnection _db;
    private readonly SettingsDBManager _settingsDBManager;
    private readonly GameSaveDBManager _gameSaveDBManager;

    private int _currentUserId = -1;

    public int GetCurrentUserId() => _currentUserId;

    public UserDbManager(SQLiteConnection db, SettingsDBManager settingsDBManager, GameSaveDBManager gameSaveDBManager)
    {
        _db = db;

        _settingsDBManager = settingsDBManager;

        _gameSaveDBManager = gameSaveDBManager;

    }

    public bool IsUserLoggedIn()
    {
        return _currentUserId != -1;
    }

    public Settings GetCurrentUserSettings()
    {
        if (!IsUserLoggedIn())
        {
            Debug.LogWarning("❌ No user logged in!");
            return null;
        }
        return _settingsDBManager.GetUserSettings(_currentUserId);
    }

    public GameSave GetCurrentUserGameSave()
    {
        return _gameSaveDBManager.GetUserGameSave(_currentUserId);
    }


    public bool UpdateCurrentUserSettings(Settings settings)
    {
        if (!IsUserLoggedIn())
        {
            Debug.LogWarning("❌ No user logged in!");
            return false;
        }

        return _settingsDBManager.UpdateSettings(settings);
    }

    public bool UpdateCurrentUserGameSave(GameSave gameSave)
    {
        return _gameSaveDBManager.UpdateGameSave(gameSave);
    }
        

    public bool CreateUser(string username, string password)
    {
        try
        {
            if (UserExists(username))
            {
                Debug.LogWarning($"User '{username}' already exists");
                return false;
            }


            var newUser = new Person
            {
                Name = username,
                Password = password
            };

            _db.Insert(newUser);

            _settingsDBManager.CreateDefaultSettings(newUser.Id);
            _gameSaveDBManager.CreateDefaultUserSave(newUser.Id);

            _currentUserId = newUser.Id;

            Debug.Log($"✅ User '{username}' created successfully with ID: {newUser.Id}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to create user: {ex.Message}");
            return false;
        }
    }

    public bool ValidateLogin(string username, string password)
    {
        try
        {
            var user = _db.Table<Person>().FirstOrDefault(u => u.Name == username);

            if (user == null)
            {
                Debug.LogWarning($"❌ User '{username}' not found");
                return false;
            }

            if (user.Password != password)
            {
                Debug.LogWarning($"❌ Invalid password for user '{username}'");
                return false;
            }

            _currentUserId = user.Id;
            Debug.Log($"✅ Login successful for user '{username}'");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Login error: {ex.Message}");
            return false;
        }
    }

    public bool UserExists(string username)
    {
        return _db.Table<Person>().Any(user => user.Name == username);
    }

    public Person GetUser(string username)
    {
        return _db.Table<Person>().FirstOrDefault(u => u.Name == username);
    }

    public bool ResetCurrentUserGameSave()
    {
        if (!IsUserLoggedIn())
        {
            Debug.LogError("❌ No user logged in!");
            return false;
        }

        return _gameSaveDBManager.ResetGameSave(GetCurrentUserId());
    }
}
