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

    [Header("Sound Details")]
    [SerializeField] private AudioSource bowAudioSource;
    [SerializeField] private AudioClip bowReleaseSound;

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

    public void StartedAiming()
    {
        aimValue = 0.0f;
        bowAudioSource.Play(0);
    }

    public void Aiming()
    {
        time += Time.deltaTime;
        aimValue = Mathf.Clamp01(time / aimTime);

        aimingDirection = aimTargetManager.GetAimDirection(shootPoint.position, Camera.main.transform.forward).normalized;
        Debug.DrawRay(shootPoint.position, aimingDirection, Color.yellow);
    }

    public void ShootArrow()
    {
        aimingDirection = aimTargetManager.GetAimDirection(shootPoint.position, Camera.main.transform.forward).normalized;

        var rot = Quaternion.LookRotation(aimingDirection, Vector3.up);
        ArrowController arrow = Instantiate(arrowPrefab, shootPoint.position, rot).GetComponent<ArrowController>();

        Vector3 shootForce = arrow.transform.forward.normalized * arrowForce * aimValue;
        arrow.AddForce(shootForce);

        AudioSource.PlayClipAtPoint(bowReleaseSound, shootPoint.position);

        ResetAimValue();
    }

    public void ResetAimValue()
    {
        bowAudioSource.Stop();
        time = 0.0f;
    }
}
