using Unity.VisualScripting;
using UnityEngine;

//примечания: его находит в другой сцене скрипт для здоровья персонажа, возможно это стоит изменить,
//и добавить посредника
public class DifficultySaver : MonoBehaviour 
{
    public static DifficultySaver Instance { get; private set; }

    public int currentDifficulty = 0;
    public int countOfDifficultyLevels = 0;

  

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
    }

    public void SetDifficulty(int difficulty)
    {
        currentDifficulty = difficulty;
    }

    public void SetCountOfDifficultyLevels(int count) 
    {
        countOfDifficultyLevels = count;
    }
  

}
