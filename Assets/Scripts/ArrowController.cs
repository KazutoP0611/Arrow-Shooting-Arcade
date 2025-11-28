using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float CheckDistance;
    [SerializeField] private float HitOffset;
    [SerializeField] private LayerMask CollisionLayers;

    private float m_Speed;

    private void OnEnable()
    {
        m_Speed = Speed;
    }

    private void Update()
    {
        if (m_Speed > 0)
        {
            var t = transform;
            if (Physics.Raycast(
                    t.position,
                    t.forward,
                    out var hitInfo,
                    CheckDistance,
                    CollisionLayers,
                    QueryTriggerInteraction.Ignore)
                )
            {
                t.position = (hitInfo.point - transform.forward) + new Vector3(0, 0, HitOffset);
                //t.position = hitInfo.point;
                m_Speed = 0;
            }
            var deltaPos = m_Speed * Time.deltaTime;
            t.position += deltaPos * t.forward;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.forward * CheckDistance);
    }
}
