using UnityEngine;

public class LevelLoadTrigger : MonoBehaviour
{
    public void LoadMainMenuTrigger() => FindObjectOfType<LevelManager>().LoadMainMenuScene();

    public void LoadLevelTrigger() => FindObjectOfType<LevelManager>().PlayLevel();

    public void QuitTrigger() => FindObjectOfType<LevelManager>().QuitGame();
}
