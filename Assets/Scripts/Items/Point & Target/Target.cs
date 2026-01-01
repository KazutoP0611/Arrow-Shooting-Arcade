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
    [Tooltip("Make sure to set target score in a composite number that can be divided by 2 and 5.")]
    [SerializeField] private int maxTaregetScore;
    [SerializeField] private int multiplyScore = 10;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip errorSound;

    [Header("Movement Details")]
    [SerializeField] private Movement movement;
    [SerializeField] private float movementLength = 1;
    [SerializeField] private float movementSpeed = 1;

    [Header("Target Details")]
    [SerializeField] private Renderer targetRender;
    [SerializeField] private Material greenHighlight;
    [SerializeField] private Material redHighlight;

    [Header("Debug")]
    [SerializeField] private float radius;

    public int GetScore { get { return maxTaregetScore; } }

    private Action<Target> OnHitAction;
    private float time;
    private Vector3 startTargetPosition = Vector3.zero;

    private void Start()
    {
        startTargetPosition = targetObject.transform.localPosition;

        targetRender.material = maxTaregetScore > 0 ? greenHighlight : redHighlight;
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

        int point = 0;
        float hitLength = Vector3.Distance(hitPoint, targetObject.transform.position);
        if (maxTaregetScore > 0)
        {
            if (hitLength < 0.07) // You hit bullseye
                point = maxTaregetScore;
            else if (hitLength < 0.3)
                point = maxTaregetScore / 2;
            else
                point = maxTaregetScore / 5;
        }
        else
        {
            if (hitLength < 0.07) // You hit bullseye
                point = Mathf.Abs(maxTaregetScore * multiplyScore);
            else
                point = maxTaregetScore;
        }

        AudioSource.PlayClipAtPoint(
            point > 0 ? hitSound : errorSound,
            transform.position
        );

        PointManager.instance.ManagePoint(point, hitPoint);

        OnHitAction?.Invoke(this);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
