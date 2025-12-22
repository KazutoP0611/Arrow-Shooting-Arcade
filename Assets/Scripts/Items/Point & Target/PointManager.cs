using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager instance { get; private set; }
    public int score { get { return currentScore; } }

    [Header("Point Display Details")]
    [SerializeField] private TextMeshProUGUI pointText;

    [SerializeField] private GameObject textPrefab;

    private int currentScore;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance.gameObject);

        instance = this;
    }

    private void Start()
    {
        currentScore = 0;
        UpdateScore();
    }

    public void ManagePoint(int scorePoint, Vector3 spawnPoint)
    {
        ScoreTextController textController = Instantiate(textPrefab, spawnPoint, Quaternion.LookRotation(Camera.main.transform.forward)).GetComponent<ScoreTextController>();
        Color textColor = scorePoint > 0 ? Color.green : Color.red;
        textController.SetText(scorePoint, textColor, OnScoreAnimatedFinish);
    }

    private void OnScoreAnimatedFinish(int score)
    {
        currentScore += score;
        UpdateScore();
    }

    private void UpdateScore() => pointText.text = currentScore.ToString();
}
