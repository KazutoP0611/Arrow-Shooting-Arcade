using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public Coroutine fadeCoroutine { get; private set; }

    [SerializeField] private CanvasGroup changeScreenPanel;
    [SerializeField] private float changeSceneInSecs;
    [SerializeField] private bool doFadeOnStart = true;

    private bool isThisInteractable;

    private void Start()
    {
        isThisInteractable = changeScreenPanel.interactable;

        if (isThisInteractable && changeScreenPanel.alpha == 0)
            changeScreenPanel.interactable = false;

        if (doFadeOnStart)
            DoFade(0, 0);
    }

    public void FadeIn(float inSecs = 0)
    {
        changeScreenPanel.blocksRaycasts = true;
        DoFade(1, inSecs);
    }

    public void FadeOut(float inSecs = 0)
    {
        DoFade(0, inSecs);
    }

    protected void DoFade(float targetAlpha, float fadeInSecs)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadingCoroutine(targetAlpha, fadeInSecs));
    }

    IEnumerator FadingCoroutine(float targetAlpha, float fadeInSecs)
    {
        yield return new WaitForSeconds(fadeInSecs);

        float timePassed = 0;
        float startAlpha = targetAlpha > 0 ? 0 : 1;

        while (timePassed < changeSceneInSecs)
        {
            timePassed += Time.deltaTime;
            changeScreenPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, timePassed / changeSceneInSecs);

            yield return null;
        }

        if (targetAlpha == 0)
            changeScreenPanel.blocksRaycasts = false;

        if (isThisInteractable)
            changeScreenPanel.interactable = targetAlpha != 0;

        //if (targetAlpha == 0)
        //{
        //    changeScreenPanel.blocksRaycasts = false;

        //    if (isThisInteractable)
        //        changeScreenPanel.interactable = false;
        //}
        //else
        //{
        //    if (isThisInteractable)
        //        changeScreenPanel.interactable = true;
        //}

        changeScreenPanel.alpha = targetAlpha;
    }
}
