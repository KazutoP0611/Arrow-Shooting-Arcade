using Unity.VisualScripting;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager instance { get; private set; }

    [SerializeField] private GameObject textPrefab;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance.gameObject);

        instance = this;
    }

    public void ManagePoint(int scorePoint, Vector3 spawnPoint)
    {
        ScoreTextController textController = Instantiate(textPrefab, spawnPoint, Quaternion.LookRotation(Camera.main.transform.forward)).GetComponent<ScoreTextController>();
        if (scorePoint > 0)
            textController.SetText($"+{scorePoint}", Color.green);
        else
            textController.SetText($"{scorePoint}", Color.red);
    }
}
