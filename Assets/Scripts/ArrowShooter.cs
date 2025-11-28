using Unity.Cinemachine.Samples;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowShooter : MonoBehaviour
{
    [Header("Aim Manager")]
    [SerializeField] private AimTargetManager aimTargetManager;

    [Header("Aim Details")]
    [SerializeField] private float arrowForce;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;

    public void ShootArrow()
    {
        Vector3 aimDirection = aimTargetManager.GetAimDirection(shootPoint.position, transform.forward).normalized;
        var rot = Quaternion.LookRotation(aimDirection, transform.up);

        GameObject arrowRigid = Instantiate(arrowPrefab, shootPoint.position, rot);

        //arrowRigid.GetComponent<Rigidbody>().AddForce(aimDirection * arrowForce, ForceMode.Impulse);
    }
}
