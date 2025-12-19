using System.Collections;
using TMPro;
using Unity.Cinemachine.Samples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    public enum GameEndType
    {
        TimeUp,
        ShootAll
    }

    public static GameSceneController instance { get; private set; }

    [Header("Timer Details")]
    [SerializeField] private float waitBeforeStartCountdown;
    [SerializeField] private float waitAfterCountdownEnded;
    [Space]
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float countdownTime;
    [SerializeField] private string textAfterCountdownEnded;

    [Header("Game Finish Details")]
    [SerializeField] private float waitForSecsAfterTargetAllOutBeforeEndGame;
    [SerializeField] private TextMeshProUGUI gameEndTextMesh;

    //private Fading fadingUI;
    private My_CursorLockManager cursorLockManager;
    private TimeCounter timeCounter;
    private TargetCount targetCount;
    private ResultScreenController resultScreenController;

    private Coroutine waitForFade;
    private float currentCountdownTime;
    private bool counting;

    private void Awake()
    {
        gameEndTextMesh.gameObject.SetActive(false);

        cursorLockManager = FindFirstObjectByType<My_CursorLockManager>();
        timeCounter = FindFirstObjectByType<TimeCounter>();
        targetCount = FindFirstObjectByType<TargetCount>();
        resultScreenController = FindFirstObjectByType<ResultScreenController>();

        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        counting = false;
        currentCountdownTime = countdownTime;

        timeCounter.Inialized(GameEnded);
        targetCount.Intialized(GameEnded);

        if (waitForFade != null)
            StopCoroutine(waitForFade);

        waitForFade = StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        // Wait for fade out screen to end.
        yield return TheArrowSceneManager.instance.GetFadeUIComponent().fadeCoroutine;

        // Wait before countdown after fade screen ended.
        yield return new WaitForSeconds(waitBeforeStartCountdown);

        // Start Countdown
        while (currentCountdownTime > 0)
        {
            currentCountdownTime -= Time.deltaTime;
            countdownText.text = $"{Mathf.CeilToInt(currentCountdownTime):0}";
            yield return null;
        }

        countdownText.text = $"{textAfterCountdownEnded}";

        yield return new WaitForSeconds(waitAfterCountdownEnded);

        countdownText.enabled = false;
        StartGame();
    }

    private void StartGame()
    {
        cursorLockManager.LockCursor();
        cursorLockManager.SetCanPushEcs(true);
    }

    [ContextMenu("Ended Game")]
    private void GameEnded(GameEndType gameEndType)
    {
        string gameEndText = "";
        switch (gameEndType)
        {
            case GameEndType.TimeUp:
                gameEndText = "time up!";
                break;
            case GameEndType.ShootAll:
                gameEndText = "finished!";
                break;
        }
        gameEndTextMesh.gameObject.SetActive(true);
        gameEndTextMesh.text = gameEndText;

        cursorLockManager.SetCanPushEcs(false);
        cursorLockManager.CursorOnGameEnded();
        resultScreenController.SetPoints(timeCounter.GetTimeLeft());
    }

    public void GotoTitleScene() => TheArrowSceneManager.instance.ChangeScene("TitleScene");

    public void RestartScene() => TheArrowSceneManager.instance.ChangeScene(SceneManager.GetActiveScene().name);
}
