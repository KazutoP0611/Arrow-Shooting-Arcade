using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] private float arrowForce;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;

    private Vector3 shootDirection;

    public void ShootArrow()
    {
        GameObject arrowRigid = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);

        Vector3 tempDirection = shootPoint.position;
        tempDirection.y = Camera.main.transform.position.y;
        shootDirection = Camera.main.transform.forward - tempDirection;
        arrowRigid.GetComponent<Rigidbody>().AddForce(shootPoint.forward * arrowForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(shootPoint.position, shootDirection);
    }
}
