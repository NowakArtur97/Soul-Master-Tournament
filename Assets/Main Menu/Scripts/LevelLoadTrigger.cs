using UnityEngine;

public class LevelLoadTrigger : MonoBehaviour
{
    private LevelManager _levelManager;

    private void Awake() => _levelManager = FindObjectOfType<LevelManager>();

    public void LoadMainMenuTrigger() => _levelManager.LoadMainMenuScene();

    public void LoadLevelTrigger() => _levelManager.PlayLevel();

    public void ReplayLevelTrigger() => _levelManager.ReplayLevel();

    public void QuitTrigger() => _levelManager.QuitGame();
}
