using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheArrowSceneManager : MonoBehaviour
{
    public static TheArrowSceneManager instance { get; private set; }

    private Coroutine changeSceneCoroutine;
    private Fading fadingUI;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //fadeUI = FindAnyObjectByType<FadeUI>();
    }

    public void ChangeScene(string sceneName)
    {
        if (changeSceneCoroutine != null)
            StopCoroutine(changeSceneCoroutine);

        changeSceneCoroutine = StartCoroutine(ChangeSceneCo(sceneName));
    }

    IEnumerator ChangeSceneCo(string sceneName)
    {
        GetFadeUIComponent().FadeIn();

        yield return GetFadeUIComponent().fadeCoroutine;

        SceneManager.LoadScene(sceneName);
    }

    public Fading GetFadeUIComponent()
    {
        if (fadingUI == null)
            fadingUI = FindFirstObjectByType<Fading>();

        return fadingUI;
    }
}
