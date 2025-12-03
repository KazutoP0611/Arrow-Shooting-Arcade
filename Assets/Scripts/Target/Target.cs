using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float radius;
    public GameObject originPointObj;
    public GameObject hitPointObj;

    private List<GameObject> trashCollector;

    private void OnEnable()
    {
        trashCollector = new List<GameObject>();
    }

    public void OnHit()
    {
        Debug.LogWarning($"Hit!!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
