using UnityEngine;

public class ArrowController : MonoBehaviour
{
    #region Move Details
    [Header("Move Details")]
    [SerializeField] private bool move;
    [SerializeField] private float Speed;
    [SerializeField] private float CheckDistance;
    [SerializeField] private float HitOffset;
    #endregion

    #region Hit Details
    [Header("Hit Details")]
    [SerializeField] private float destroyArrowAfterHitTarget;
    [SerializeField] private LayerMask CollisionLayers;
    #endregion

    private Rigidbody rb;
    private float m_Speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_Speed = Speed;
    }

    private void Update()
    {
        if (m_Speed > 0)
        {
            var t = transform;

            CheckHitTarget(t);

            if (move)
                MoveArrow(t);
        }
    }

    private void MoveArrow(Transform t)
    {
        var deltaPos = m_Speed * Time.deltaTime;
        t.position += deltaPos * t.forward;
    }

    private void CheckHitTarget(Transform t)
    {
        if (Physics.Raycast(
                    t.position,
                    t.forward,
                    out var hitInfo,
                    CheckDistance,
                    CollisionLayers,
                    QueryTriggerInteraction.Ignore)
                )
        {
            //t.position = (hitInfo.point - transform.position) + new Vector3(0, 0, HitOffset);
            t.position = hitInfo.point;
            m_Speed = 0;
            rb.useGravity = false;
            rb.isKinematic = true;

            Target target = hitInfo.collider.GetComponentInParent<Target>();

            if (target)
            {
                target.OnHit(hitInfo.point, gameObject);
                //Destroy(gameObject, destroyArrowAfterHitTarget);
            }
            else
                Destroy(gameObject, destroyArrowAfterHitTarget);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.forward * CheckDistance);
    }
}
