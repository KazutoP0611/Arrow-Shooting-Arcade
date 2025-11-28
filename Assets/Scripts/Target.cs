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

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
