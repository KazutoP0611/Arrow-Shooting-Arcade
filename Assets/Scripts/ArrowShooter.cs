using Unity.Cinemachine.Samples;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowShooter : MonoBehaviour
{
    [Header("Aim Manager")]
    public AimTargetManager aimTargetManager;

    [Header("Aim Details")]
    [SerializeField] private float arrowForce;
    [SerializeField] private float aimTime;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;

    [Header("Debug Only")]
    public Transform GetShootPoint { get => shootPoint; }
    public Vector3 AimingDirection { get => aimingDirection; }

    private Vector3 aimingDirection;
    private float aimValue;
    private float time;

    private void Start()
    {
        ResetAimValue();
    }

    public void Aiming()
    {
        time += Time.deltaTime;
        aimValue = Mathf.Clamp01(time / aimTime);
        //Debug.LogWarning($"Aim Value is : {aimValue}\n");
    }

    public void ShootArrow()
    {
        //Debug.LogWarning($"Shoot!\n");
        aimingDirection = aimTargetManager.GetAimDirection(shootPoint.position, Camera.main.transform.forward).normalized;

        var rot = Quaternion.LookRotation(aimingDirection, Vector3.up);
        GameObject arrowRigid = Instantiate(arrowPrefab, shootPoint.position, rot);

        Vector3 shootForce = arrowRigid.transform.forward.normalized * arrowForce * aimValue;
        //Debug.LogWarning(shootForce);
        arrowRigid.GetComponent<Rigidbody>().AddForce(shootForce, ForceMode.Impulse);

        ResetAimValue();
    }

    private void ResetAimValue()
    {
        aimValue = 0.0f;
        time = 0.0f;
    }
}
