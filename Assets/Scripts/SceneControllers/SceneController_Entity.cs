using UnityEngine;

public class SceneController_Entity : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;

    protected void ChangeSceneSequence(string changeToSceneName)
    {
        StopBGM();
        TheArrowSceneManager.instance.ChangeScene(changeToSceneName);
    }

    public void PlayBGM() => bgmAudioSource.Play();

    public void PauseBGM() => bgmAudioSource.Pause();

    public void UnPauseBGM() => bgmAudioSource.UnPause();

    public void StopBGM() => bgmAudioSource.Stop();
}
