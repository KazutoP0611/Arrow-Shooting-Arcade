using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private int fullTime;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Slider timeSlider;

    private float currentTime;
    private bool counting = false;

    private void Start()
    {
        currentTime = fullTime;
        UpdateTimeText();
    }

    [ContextMenu("Start CountDown")]
    public void StartCount() => counting = true;

    private void Update()
    {
        if (counting)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                currentTime = Mathf.Max(currentTime, 0);
                UpdateTimeText();
            }
            else
                counting = false;
        }
    }

    private void UpdateTimeText()
    {
        int min = Mathf.FloorToInt(currentTime / 60f);
        int secs = Mathf.CeilToInt(currentTime % 60f);
        timeText.text = $"{min}:{secs:00}";

        timeSlider.value = currentTime / fullTime;
    }
}
