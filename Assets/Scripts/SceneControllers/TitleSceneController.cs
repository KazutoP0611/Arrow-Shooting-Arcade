using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    public void GotoGameScene() => TheArrowSceneManager.instance.ChangeScene("Scene1");
}
