using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float radius;

    [SerializeField] private int targetScore;

    private List<GameObject> trashCollector;

    private void OnEnable()
    {
        trashCollector = new List<GameObject>();
    }

    public void OnHit(Vector3 hitPoint)
    {
        float hitLength = Vector3.Distance(hitPoint, transform.position);
        //Debug.LogWarning(hitLength);
        int point = 0;
        if (hitLength < 0.07)
            point = targetScore;
        else if (hitLength < 0.3)
            point = targetScore / 2;
        else
            point = targetScore / 5;

        PointManager.instance.ManagePoint(point, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
