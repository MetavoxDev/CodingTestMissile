using UnityEngine;

public class UserGameplayInputs : MonoBehaviour
{
    [SerializeField] private TargetsPointedQueue targetsQueue = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnCrossSign(LevelMaster.Instance.GameplayCamera.ScreenToWorldPoint((Vector2)Input.mousePosition));
        }
        else
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    SpawnCrossSign(LevelMaster.Instance.GameplayCamera.ScreenToWorldPoint(touch.position));
                }
            }
        }
    }

    private void SpawnCrossSign(Vector3 _worldPos)
    {
        Vector2 worldPosToUse = new Vector2(_worldPos.x, _worldPos.y);
        GameObject sign = LevelMaster.Instance.CrossesPoolingSystem.GetPooledObject();
        if (sign != null)
        {
            sign.transform.position = worldPosToUse;
            sign.SetActive(true);
        }
        targetsQueue.AddPointedPosition(in worldPosToUse, sign);
    }
}
