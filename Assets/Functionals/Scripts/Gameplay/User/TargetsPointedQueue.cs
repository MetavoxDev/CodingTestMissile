using System.Collections.Generic;
using UnityEngine;

public struct PointedPosition
{
    public Vector2 pos;
    public GameObject sign;

    public PointedPosition(Vector3 _pos, GameObject _sign)
    {
        pos = _pos;
        sign = _sign;
    }
}

public class TargetsPointedQueue : MonoBehaviour
{
    private Queue<PointedPosition> pointedPositions = null;

    private void Start()
    {
        pointedPositions = new Queue<PointedPosition>();

        TowerController[] towers = LevelMaster.Instance.Towers;
        for (int i = 0; i < towers.Length; i++)
        {
            towers[i].OnFinishReloading -= UsePointPosition;
            towers[i].OnFinishReloading += UsePointPosition;
        }
    }

    private bool CanDirectlyUseTargetPos(in Vector2 _pos, GameObject _sign = null)
    {
        TowerController[] towers = LevelMaster.Instance.Towers;
        for (int i = 0; i < towers.Length; i++)
        {
            if(towers[i].IsReloading() == false)
            {
                towers[i].LaunchMissile(new PointedPosition(_pos, _sign));
                return true;
            }
        }
        return false;
    }
    public void AddPointedPosition(in Vector2 _pos, GameObject _sign = null)
    {
        if (!CanDirectlyUseTargetPos(in _pos, _sign))
        {
            pointedPositions.Enqueue(new PointedPosition(_pos, _sign));
        }
    }

    private void UsePointPosition(TowerController _tower)
    {
        if(pointedPositions.Count > 0)
            _tower.LaunchMissile(pointedPositions.Dequeue());
    }
}
