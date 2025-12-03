using TMPro;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    [Header("Transform Details")]
    [SerializeField] private Vector3 goToDirection;
    [SerializeField] private float transformInSecs;

    [Header("Text Details")]
    [SerializeField] private TextMeshProUGUI text;

    private Vector3 startPosition;
    private Vector3 targetPoint;
    private float time;

    private void Start()
    {
        startPosition = transform.position;
        targetPoint = transform.position + goToDirection;
    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, targetPoint, time / transformInSecs);
    }

    public void SetText(string textString, Color color)
    {
        text.color = color;
        text.text = textString;
    }
}
