using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] private float arrowForce;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;

    public void ShootArrow()
    {
        GameObject arrowRigid = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        arrowRigid.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * arrowForce, ForceMode.Impulse);
    }
}
