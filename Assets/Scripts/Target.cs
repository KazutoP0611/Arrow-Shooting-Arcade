using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning($"{collision.transform.position - transform.position}");
    }
}
