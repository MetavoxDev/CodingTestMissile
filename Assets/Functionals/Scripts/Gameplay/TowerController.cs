using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TowerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform muzzlePosition = null;

    [Header("Loading Module")]
    [SerializeField] private Transform gaugeLoadingSign = null;
    [SerializeField] private AnimationCurve gaugeFillingCurve = null;

    [Header("Tweaks")]
    [SerializeField] private float loadTime = 2f;
    [SerializeField] private float missilesSpeed = 2f;

    private Coroutine loadingCoroutine = null;

    public UnityAction<TowerController> OnFinishReloading = null;

    public void LaunchMissile(in PointedPosition _targetPos)
    {
        if (IsReloading()) return;

        //Spawn Missile
        GameObject missile = LevelMaster.Instance.MissilesPoolingSystem.GetPooledObject();
        if (missile != null)
        {
            missile.SetActive(true);
            missile.transform.position = muzzlePosition.position;
            missile.GetComponent<MissileController>().Launch(in _targetPos, missilesSpeed);
        }

        Reload();
    }
    public void LaunchMissile(in Vector2 _targetPos)
    {
        if (IsReloading()) return;

        //Spawn Missile
        GameObject missile = LevelMaster.Instance.MissilesPoolingSystem.GetPooledObject();
        if (missile != null)
        {
            Debug.Log("Set Missile");
            missile.SetActive(true);
            missile.transform.position = muzzlePosition.position;
            missile.GetComponent<MissileController>().Launch(in _targetPos, missilesSpeed);
        }

        Reload();
    }

    //Loading
    public bool IsReloading()
    {
        return (loadingCoroutine != null);
    }
    private void Reload()
    {
        if (loadingCoroutine != null)
            StopCoroutine(loadingCoroutine);

        loadingCoroutine = StartCoroutine(LoadingTime());
    }
    private IEnumerator LoadingTime()
    {
        float elapsedTime = 0;

        while (elapsedTime < loadTime)
        {
            gaugeLoadingSign.localScale = new Vector3(gaugeLoadingSign.localScale.x, gaugeFillingCurve.Evaluate(elapsedTime/loadTime), gaugeLoadingSign.localScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gaugeLoadingSign.localScale = new Vector3(gaugeLoadingSign.localScale.x, gaugeFillingCurve.Evaluate(1), gaugeLoadingSign.localScale.z);

        loadingCoroutine = null;
        OnFinishReloading?.Invoke(this);
    }
}
