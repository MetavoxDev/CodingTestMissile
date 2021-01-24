using CustomKit;
using UnityEngine;

public class LevelMaster:Singleton<LevelMaster>
{
    [SerializeField] private Camera gameplayCamera = null;
    public Camera GameplayCamera
    {
        get
        {
            return gameplayCamera;
        }
    }

    [SerializeField] private TowerController[] towers = null;
    public TowerController[] Towers
    {
        get
        {
            return towers;
        }
    }

    [SerializeField] private ObjectsPooler crossesPoolingSystem = null;
    public ObjectsPooler CrossesPoolingSystem
    {
        get
        {
            return crossesPoolingSystem;
        }
    }

    [SerializeField] private ObjectsPooler missilesPoolingSystem = null;
    public ObjectsPooler MissilesPoolingSystem
    {
        get
        {
            return missilesPoolingSystem;
        }
    }

    [SerializeField] private ObjectsPooler explosionsPoolingSystem = null;
    public ObjectsPooler ExplosionsPoolingSystem
    {
        get
        {
            return explosionsPoolingSystem;
        }
    }

    public void Init(LevelData _data)
    {

    }
}
