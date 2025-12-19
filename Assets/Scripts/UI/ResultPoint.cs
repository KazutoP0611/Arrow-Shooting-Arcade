using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ResultPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private float waitForSecsAfterCoroutineEnded = 0.35f;

    public Coroutine coroutine { get; private set; }

    private int pointSum;

    // You can use what you want.
    // 1. First coroutine way
    //public Coroutine ShowResultPoint(int gotPoint)
    //{
    //    pointSum = 0;

    //    if (coroutine != null)
    //        StopCoroutine(coroutine);
    //    coroutine = StartCoroutine(ShowTextCoroutine(gotPoint));

    //    return coroutine;
    //}

    // 2. Second coroutine way
    public void ShowResultPoint(int gotPoint)
    {
        pointSum = 0;

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(ShowTextCoroutine(gotPoint));
    }

    IEnumerator ShowTextCoroutine(int gotPoint)
    {
        while (pointSum < gotPoint)
        {
            pointSum++;
            resultText.text = pointSum.ToString();
            yield return null;
        }
        yield return new WaitForSeconds(waitForSecsAfterCoroutineEnded);
    }
}
