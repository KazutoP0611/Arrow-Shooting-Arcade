using System.Collections;
using TMPro;
using UnityEngine;

public class ResultScreenController : MonoBehaviour
{
    [SerializeField] private Fading resultScreenFade;

    private Coroutine resultFadeCo;

    //[SerializeField] private TextMeshProUGUI result
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
    }

    public void ShowResult(int points, int timeLeft)
    {

    }
}
