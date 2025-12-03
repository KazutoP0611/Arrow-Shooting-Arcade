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

    public void OnHit()
    {
         PointManager.instance.ManagePoint(targetScore, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
