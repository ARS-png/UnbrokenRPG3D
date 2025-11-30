using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager: MonoBehaviour 
{
    public static _SceneManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        HealthSystem.OnPlayerDie += () => StartCoroutine(LoadGameSceneAfterDelay("UIScene", 3f));
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");    
    }

    

    public IEnumerator LoadGameSceneAfterDelay(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(sceneName);
    }



    public void LoadMenuScene()
    {
        SceneManager.LoadScene("UIScene");
    }

    public void ExitGame()
    { 
        Application.Quit();
    }

}
