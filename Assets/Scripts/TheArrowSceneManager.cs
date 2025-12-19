using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheArrowSceneManager : MonoBehaviour
{
    [SerializeField] private AudioSource changeSceneAudioSource;

    public static TheArrowSceneManager instance { get; private set; }

    private Fading fadingUI;
    private Coroutine changeSceneCoroutine;

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

        fadingUI = FindAnyObjectByType<ChangeScreenFadeUI>();
    }

    public void ChangeScene(string sceneName)
    {
        //Cursor.lockState = CursorLockMode.Locked;
        changeSceneAudioSource.Play(0);

        if (changeSceneCoroutine != null)
            StopCoroutine(changeSceneCoroutine);

        changeSceneCoroutine = StartCoroutine(ChangeSceneCo(sceneName));
    }

    IEnumerator ChangeSceneCo(string sceneName)
    {
        GetFadeUIComponent().FadeIn();

        yield return GetFadeUIComponent().fadeCoroutine;

        changeSceneAudioSource.Stop();
        SceneManager.LoadScene(sceneName);
    }

    public Fading GetFadeUIComponent()
    {
        if (fadingUI == null)
            fadingUI = FindFirstObjectByType<ChangeScreenFadeUI>();

        return fadingUI;
    }
}
