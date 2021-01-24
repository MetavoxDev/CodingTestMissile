using UnityEngine;

public class MissileController : MonoBehaviour
{
    private const float SECURE_GARBAGE_DESTROYING_DST = 100f;

    [Header("Refs")]
    [SerializeField] private Rigidbody2D rigidBody = null;
    [SerializeField] private CircleCollider2D circleCollider = null;
    [SerializeField] private Transform visualBody = null;

    private bool isDead = true; //Prevent spawning detection before launch
    private Vector2 startingPos = default;
    private Vector2? currentTargetPos = null;

    private GameObject SignToDelete = null;

    //--------------------------------------Main Logic
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isDead) return;

    //    Kill();
    //}
    //private void LateUpdate()
    //{
    //    if (isDead || currentTargetPos.HasValue == false) return;


    //}
    private void FixedUpdate()
    {
        if (isDead) return;

        if (Vector2.Distance(rigidBody.position, startingPos) >= SECURE_GARBAGE_DESTROYING_DST)
        {
            Kill();
        }

        if (currentTargetPos.HasValue == false) return;
        if (Vector2.Distance(rigidBody.position, currentTargetPos.Value) <= circleCollider.radius)
        {
            Kill();
        }
    }

    //-------------------------------------Functionalities
    public void Launch(in Vector2 _targetPos, float _speed = 1f)
    {
        isDead = false;
        startingPos = this.transform.position;
        currentTargetPos = _targetPos;
        rigidBody.velocity = (_targetPos - (Vector2)(transform.position)).normalized * _speed;
        visualBody.rotation = Quaternion.FromToRotation(visualBody.position, rigidBody.velocity);
    }
    public void Launch(in PointedPosition _targetPos, float _speed = 1f)
    {
        isDead = false;
        startingPos = this.transform.position;
        currentTargetPos = _targetPos.pos;
        SignToDelete = _targetPos.sign;
        rigidBody.velocity = (_targetPos.pos - (Vector2)(transform.position)).normalized * _speed;
        visualBody.rotation = Quaternion.FromToRotation(visualBody.position, rigidBody.velocity);
    }
    private void Kill() //Destroy the missile and spawn explosion
    {
        if (SignToDelete != null)
            SignToDelete.SetActive(false);

        isDead = true;
        this.gameObject.SetActive(false);
    }
}
