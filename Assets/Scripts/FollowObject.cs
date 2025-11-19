using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private Vector3 followOffset;

    private void Update()
    {
        transform.position = followObject.transform.position + followOffset;   
    }
}
