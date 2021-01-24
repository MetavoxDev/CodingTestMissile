using CustomKit;
using UnityEngine;

public class GameMaster : SingletonPersistent<GameMaster>
{
    public static new GameMaster Instance
    {
        get
        {
            if (!instance)
            {
                instance = CreateInstanceDynamically();
            }
            return instance;
        }
    }
    private static GameMaster CreateInstanceDynamically()
    {
        GameObject go = new GameObject("GameMaster");
        GameMaster gm = go.AddComponent<GameMaster>();

        return gm;
    }

    //-----------------------------------------LEVELS LOGIC
    [SerializeField] private LevelLibrary levels = null;

    private LevelMaster levelManager = null;
    public LevelMaster LevelManager { get { return levelManager; } }

    public void SetupLevel(int _index)
    {

    }
}