using UnityEngine;
using System.Collections;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(InitializeGameData());
    }

    private IEnumerator InitializeGameData()
    {
        Debug.Log("🔄 GameInitializer: Starting initialization...");

    
        while (GameDataManager.Instance == null)
        {
            yield return null; 
        }
        Debug.Log("✅ GameDataManager found");

  
        while (DatabaseManager.Instance == null)
        {
            yield return null;
        }
        Debug.Log("✅ DatabaseManager found");

   
        while (!DatabaseManager.Instance.IsUserLoggedIn())
        {
            yield return null;
        }
        Debug.Log("✅ User is logged in");


        GameDataManager.Instance.LoadPlayerPosition();

        Debug.Log("🎮 Game initialization completed!");
    }
}