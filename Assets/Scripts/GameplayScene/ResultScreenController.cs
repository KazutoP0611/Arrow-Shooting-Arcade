using System.Collections;
using TMPro;
using UnityEngine;

public class ResultScreenController : MonoBehaviour
{
    [SerializeField] private Fading resultScreenFade;

    [Header("Text Details")]
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private TextMeshProUGUI totalText;

    private Coroutine resultFadeCo;
    private int score;
    private int timeLeftScore;
    private int totalScore;

    public void SetPoints(int timeLeft)
    {
        timeLeftScore = (timeLeft * 3);
    }

    public void ShowGameResult()
    {
        if (resultFadeCo != null)
            StopCoroutine(resultFadeCo);
        StartCoroutine(ResultFadeCoroutine());
    }

    IEnumerator ResultFadeCoroutine()
    {
        resultScreenFade.FadeIn(1.4f);

        yield return resultScreenFade.fadeCoroutine;

        ShowPoint();
    }

    private void ShowPoint()
    {
        score = PointManager.instance.score;
        totalScore = (score + timeLeftScore);

        pointText.text = score.ToString();
        timeLeftText.text = timeLeftScore.ToString();
        totalText.text = totalScore.ToString();
    }
}
