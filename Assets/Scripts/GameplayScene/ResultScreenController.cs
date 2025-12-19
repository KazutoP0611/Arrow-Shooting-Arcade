using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreenController : MonoBehaviour
{
    [SerializeField] private Fading resultScreenFade;

    [Header("Text Details")]
    [SerializeField] private List<ResultPoint> listOfResultPoint;
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
        resultFadeCo = StartCoroutine(ResultFadeCoroutine());
    }

    IEnumerator ResultFadeCoroutine()
    {
        resultScreenFade.FadeIn(1.4f);

        yield return resultScreenFade.fadeCoroutine;

        // Calculate score
        score = PointManager.instance.score;
        totalScore = (score + timeLeftScore);

        #region Show Each point result with animations

        #region First methos to show text animation
        for (int i = 0; i < 3; i++)
        {
            int resultPoint = 0;
            switch (i)
            {
                case 0:
                    resultPoint = score;
                    break;
                case 1:
                    resultPoint = timeLeftScore;
                    break;
                case 2:
                    resultPoint = totalScore;
                    break;
            }
            // 1. First coroutine way
            //yield return listOfResultPoint[i].ShowResultPoint(resultPoint);

            // 2. Second coroutine way
            listOfResultPoint[i].ShowResultPoint(resultPoint);
            yield return listOfResultPoint[i].coroutine;
        }
        #endregion

        //You can use either one above and below, same result

        #region Second method to show text animation
        //listOfResultPoint[0].ShowResultPoint(score);
        //yield return listOfResultPoint[0].coroutine;
        //listOfResultPoint[1].ShowResultPoint(timeLeftScore);
        //yield return listOfResultPoint[1].coroutine;
        //listOfResultPoint[2].ShowResultPoint(totalScore);
        //yield return listOfResultPoint[2].coroutine;
        #endregion

        #endregion
    }

    //private void ShowPoint()
    //{
    //    score = PointManager.instance.score;
    //    totalScore = (score + timeLeftScore);

    //    if (showResultPointCo != null)
    //        StopCoroutine(showResultPointCo);
    //    StartCoroutine(ShowResultPoints(score, timeLeftScore, totalScore));
    //}

    //IEnumerator ShowResultPoints(int score, int timeLeftScore, int totalScore)
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        int resultPoint = 0;
    //        switch (i)
    //        {
    //            case 0:
    //                resultPoint = score;
    //                break;
    //            case 1:
    //                resultPoint = timeLeftScore;
    //                break;
    //            case 2:
    //                resultPoint = totalScore;
    //                break;
    //        }
    //        // 1. First coroutine way
    //        //yield return listOfResultPoint[i].ShowResultPoint(resultPoint);

    //        // 2. Second coroutine way
    //        listOfResultPoint[i].ShowResultPoint(resultPoint);
    //        yield return listOfResultPoint[i].coroutine;
    //    }


    //    //You can use either one above and below, same result

    //    //yield return listOfResultPoint[0].ShowResultPoint(score);
    //    //yield return listOfResultPoint[1].ShowResultPoint(timeLeftScore);
    //    //yield return listOfResultPoint[2].ShowResultPoint(totalScore);
    //}
}
