using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float radius;

    [SerializeField] private int maxTaregetScore;
    [SerializeField] private AudioClip hitSound;

    private List<GameObject> trashCollector;

    private void OnEnable()
    {
        trashCollector = new List<GameObject>();
    }

    public void OnHit(Vector3 hitPoint, GameObject arrow)
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position);

        float hitLength = Vector3.Distance(hitPoint, transform.position);
        //Debug.LogWarning(hitLength);
        int point = 0;
        if (hitLength < 0.07)
            point = maxTaregetScore;
        else if (hitLength < 0.3)
            point = maxTaregetScore / 2;
        else
            point = maxTaregetScore / 5;

        PointManager.instance.ManagePoint(point, transform.position);

        Destroy(arrow);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
