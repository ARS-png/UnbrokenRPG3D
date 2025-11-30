using UnityEngine;

public class GameDataSaver : MonoBehaviour
{
    public static GameDataSaver Instance;
    [SerializeField] Transform player;

    int x;
    int y;
    int z;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPlayerPosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
