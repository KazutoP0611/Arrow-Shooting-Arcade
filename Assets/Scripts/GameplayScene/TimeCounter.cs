using System;
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
    private Action onTimeUp;

    private int min;
    private int secs;

    private void Start()
    {
        currentTime = fullTime;
        UpdateTimeText();
    }

    public void Inialized(Action onTimeUpCallback)
    {
        onTimeUp = onTimeUpCallback;
    }

    //[ContextMenu("Start CountDown")]
    //private void StartCount() => counting = true;

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
            {
                counting = false;
                onTimeUp?.Invoke();
            }
        }
    }

    public void SetCounting(bool counting) => this.counting = counting;

    private void UpdateTimeText()
    {
        min = Mathf.FloorToInt(currentTime / 60f);
        secs = Mathf.CeilToInt(currentTime % 60f);
        timeText.text = $"{min}:{secs:00}";

        timeSlider.value = currentTime / fullTime;
    }

    public int GetTimeLeft()
    {
        return (min * 60) + secs; 
    }

    //private void GetTime(int out min, int out secs)
    //{
    //    min = Mathf.FloorToInt(currentTime / 60f);
    //    secs = Mathf.CeilToInt(currentTime % 60f);

    //    return GetTime(min, secs);
    //}
}
