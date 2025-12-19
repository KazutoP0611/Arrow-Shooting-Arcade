using UnityEngine;

public class TitleSceneController : SceneController_Entity
{
    public void GotoGameScene() => ChangeSceneSequence("Scene1");
}
