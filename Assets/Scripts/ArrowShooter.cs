using Unity.Cinemachine.Samples;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowShooter : MonoBehaviour
{
    [Header("Aim Manager")]
    public AimTargetManager aimTargetManager;

    [Header("Aim Details")]
    [SerializeField] private float arrowForce;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;

    [Header("Debug Only")]
    public Transform GetShootPoint { get => shootPoint; }
    public Vector3 AimingDirection { get => aimingDirection; }


    private Vector3 aimingDirection;

    public void ShootArrow()
    {
        aimingDirection = aimTargetManager.GetAimDirection(shootPoint.position, Camera.main.transform.forward).normalized;

        var rot = Quaternion.LookRotation(aimingDirection, Vector3.up);

        GameObject arrowRigid = Instantiate(arrowPrefab, shootPoint.position, rot);

        //arrowRigid.GetComponent<Rigidbody>().AddForce(aimDirection * arrowForce, ForceMode.Impulse);
    }
}
