using System;
using TMPro;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    //public delegate void OnAnimatedFinished(int score);
    //public OnAnimatedFinished onAnimtedFinished;

    [Header("Transform Details")]
    [SerializeField] private Vector3 goToDirection;
    [SerializeField] private float transformInSecs;

    [Header("Text Details")]
    [SerializeField] private TextMeshProUGUI text;

    private Vector3 startPosition;
    private Vector3 targetPoint;
    private float time;

    private int score;
    private Action<int> onAnimtedFinished;

    private void Start()
    {
        startPosition = transform.position;
        targetPoint = transform.position + goToDirection;
    }

    private void Update()
    {
        time += Time.deltaTime;
        float t = time / transformInSecs;
        transform.position = Vector3.Lerp(startPosition, targetPoint, time / transformInSecs);

        if (t >= 1)
        {
            onAnimtedFinished?.Invoke(score);
            Destroy(gameObject);
        }
    }

    public void SetText(int score, Color color, Action<int> OnTextAnimatedFinish)
    {
        this.score = score;
        text.text = score.ToString();
        text.color = color;
        onAnimtedFinished = OnTextAnimatedFinish;
    }
}
