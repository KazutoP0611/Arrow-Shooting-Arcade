using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public Coroutine fadeCoroutine { get; private set; }

    [SerializeField] private float changeSceneInSecs;
    [SerializeField] private bool doFadeOnStart = true;

    private CanvasGroup changeScreenPanel;

    protected virtual void Awake()
    {
        changeScreenPanel = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (doFadeOnStart)
            DoFade(0);
    }

    public void FadeIn()
    {
        DoFade(1);
    }

    public void FadeOut()
    {
        DoFade(0);
    }

    protected void DoFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadingCoroutine(targetAlpha));
    }

    IEnumerator FadingCoroutine(float targetAlpha)
    {
        float timePassed = 0;
        float startAlpha = changeScreenPanel.alpha;

        while (timePassed < changeSceneInSecs)
        {
            timePassed += Time.deltaTime;
            changeScreenPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, timePassed / changeSceneInSecs);

            yield return null;
        }

        changeScreenPanel.alpha = targetAlpha;
    }
}
