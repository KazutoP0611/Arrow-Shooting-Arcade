using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum Movement
    {
        None,
        Side,
        Vertical,
        Depth
    }

    [Header("General Details")]
    [SerializeField] private int maxTaregetScore;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private AudioClip hitSound;

    [Header("Movement Details")]
    [SerializeField] private Movement movement;
    [SerializeField] private float movementLength = 1;
    [SerializeField] private float movementSpeed = 1;

    [Header("Debug")]
    [SerializeField] private float radius;

    private Action<Target> OnHitAction;
    private float time;
    private Vector3 startTargetPosition = Vector3.zero;
    private bool moving;

    private void Start()
    {
        startTargetPosition = targetObject.transform.localPosition;
    }

    public void Initialized(Action<Target> OnHitCallback)
    {
        OnHitAction = OnHitCallback;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movement != Movement.None)
        {
            time += Time.deltaTime;
            float movementAxis = Mathf.Sin(time * movementSpeed) * movementLength;

            Vector3 movementPosition = Vector3.zero;

            switch (movement)
            {
                case Movement.Side:
                    movementPosition = Vector3.right * movementAxis;
                    break;
                case Movement.Vertical:
                    movementPosition = Vector3.up * movementAxis;
                    break;
                case Movement.Depth:
                    movementPosition = Vector3.forward * movementAxis;
                    break;
            }

            targetObject.transform.localPosition = startTargetPosition + movementPosition;
        }
    }

    public void OnHit(Vector3 hitPoint, GameObject arrow)
    {
        arrow.transform.parent = transform;

        AudioSource.PlayClipAtPoint(hitSound, transform.position);

        float hitLength = Vector3.Distance(hitPoint, transform.position);
        
        int point = 0;
        if (hitLength < 0.07)
            point = maxTaregetScore;
        else if (hitLength < 0.3)
            point = maxTaregetScore / 2;
        else
            point = maxTaregetScore / 5;

        PointManager.instance.ManagePoint(point, transform.position);

        OnHitAction?.Invoke(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
