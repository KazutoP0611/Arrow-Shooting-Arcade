using System.Collections;
using TMPro;
using Unity.Cinemachine.Samples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [Header("Timer Details")]
    [SerializeField] private float countdownTime;
    [SerializeField] private float waitUntilStartCountdown;
    [SerializeField] private TextMeshProUGUI countdownText;

    //private Fading fadingUI;
    private My_CursorLockManager cursorLockManager;
    private TimeCounter timeCounter;

    private Coroutine waitForFade;
    private Coroutine waitBeforeCountdownCo;
    private float currentCountdownTime;
    private bool counting;

    private void Awake()
    {
        cursorLockManager = FindFirstObjectByType<My_CursorLockManager>();
        timeCounter = FindFirstObjectByType<TimeCounter>();
    }

    private void Start()
    {
        counting = false;
        currentCountdownTime = countdownTime;
        timeCounter.Inialized(GameEnded);

        if (waitForFade != null)
            StopCoroutine(waitForFade);

        waitForFade = StartCoroutine(WaitForFadeCo());
    }

    private void Update()
    {
        if (counting)
        {
            CountdownBeforeStart();
        }
    }

    private void CountdownBeforeStart()
    {
        if (currentCountdownTime > 0)
        {
            currentCountdownTime -= Time.deltaTime;
            countdownText.text = $"{Mathf.CeilToInt(currentCountdownTime):0}";
        }
        else
        {
            counting = false;
            countdownText.enabled = false;

            StartGame();
        }
    }

    private void StartGame()
    {
        cursorLockManager.LockCursor();
        cursorLockManager.SetCanPushEcs(true);
        timeCounter.StartCount();
    }

    [ContextMenu("Ended Game")]
    private void GameEnded()
    {
        cursorLockManager.SetCanPushEcs(false);
        cursorLockManager.CursorOnGameEnded();
    }

    IEnumerator WaitForFadeCo()
    {
        yield return TheArrowSceneManager.instance.GetFadeUIComponent().fadeCoroutine;

        if (waitBeforeCountdownCo != null)
            StopCoroutine(waitBeforeCountdownCo);

        waitBeforeCountdownCo = StartCoroutine(WaitBeforeCountdown());
    }

    IEnumerator WaitBeforeCountdown()
    {
        yield return new WaitForSeconds(waitUntilStartCountdown);
        counting = true;
    }

    public void GotoTitleScene() => TheArrowSceneManager.instance.ChangeScene("TitleScene");

    public void RestartScene() => TheArrowSceneManager.instance.ChangeScene(SceneManager.GetActiveScene().name);
}
