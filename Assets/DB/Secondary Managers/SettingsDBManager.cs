using SQLite;
using System;
using System.Linq;
using UnityEngine;

public class SettingsDBManager
{
    private readonly SQLiteConnection _db;

    public SettingsDBManager(SQLiteConnection db)
    {
        _db = db;
    }

    public bool CreateDefaultSettings(int userId)
    {
        try
        {
            var defaultSettings = new Settings
            {
                UserID = userId,
                QualityLevel = 0,
                GameVolume = 1

            };

            _db.Insert(defaultSettings);
            Debug.Log($"✅ Default settings created for user ID: {userId}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to create default settings: {ex.Message}");
            return false;
        }
    }

    public Settings GetUserSettings(int userId)
    {
        try
        {
            var settings = _db.Table<Settings>().FirstOrDefault(s => s.UserID == userId);

            if (settings == null)
            {
                CreateDefaultSettings(userId);
                settings = _db.Table<Settings>().FirstOrDefault(s => s.UserID == userId);
            }

            return settings;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to get user settings: {ex.Message}");
            return null;
        }
    }

    public bool UpdateSettings(Settings settings) // чтобы изменять объекты не толькоо в памяти но и в бд
    {
        try
        {
            _db.Update(settings);
            Debug.Log($"✅ Settings updated for user ID: {settings.UserID}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to update settings: {ex.Message}");
            return false;
        }
    }


}
