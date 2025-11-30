using SQLite;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameSaveDBManager
{
    private readonly SQLiteConnection _db;

    public GameSaveDBManager(SQLiteConnection db)
    {
        _db = db;
    }

    public bool CreateDefaultUserSave(int userId)
    {
        try
        {
            var defaultGameSave = new GameSave
            {
                UserID = userId,
                XPosition = 0,
                YPosition = 30,
                ZPosition = 0
            };

            _db.Insert(defaultGameSave);
            Debug.Log($"✅ Default game_save created for user ID: {userId}");
            return true;


        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to create default game_save: {ex.Message}");
            return false;
        }
    }

    public GameSave GetUserGameSave(int userId)
    {
        try
        {
            var gameSave = _db.Table<GameSave>().FirstOrDefault(x => x.UserID == userId);

            if (gameSave == null)
            {
                CreateDefaultUserSave(userId);
                gameSave = _db.Table<GameSave>().FirstOrDefault(x => x.UserID == userId);
            }

            return gameSave;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to get user_save: {ex.Message}");
            return null;
        }
    }

    public bool UpdateGameSave(GameSave gameSave)
    {
        try
        {
            _db.Update(gameSave);
            Debug.Log($"✅ Game Save updated for user ID: {gameSave.UserID}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to update gameSave: {ex.Message}");
            return false;

        }
    }

    public bool ResetGameSave(int userId)
    {
        try
        {
            var gameSave = _db.Table<GameSave>().FirstOrDefault(x => x.UserID == userId);

            if (gameSave != null) // магические числа, изменитьы
            {
                gameSave.XPosition = 10;
                gameSave.YPosition = -1;
                gameSave.ZPosition = 30;

                _db.Update(gameSave);
                Debug.Log($"✅ Game save reset to default for user ID: {userId}");
            }
            else
            {
                CreateDefaultUserSave(userId);
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Failed to reset game save: {ex.Message}");
            return false;
        }
    }




}

