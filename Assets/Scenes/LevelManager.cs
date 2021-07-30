using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const string WINNING_SCENE_NAME = "Winning Scene";

    public static LevelManager Instance { get; private set; }
    public Player Winner { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayLevel()
    {
        // TODO: LevelManager: Load chosen level
        SceneManager.LoadScene("Level 1");
    }

    public void LoadWinningScene() => SceneManager.LoadScene(WINNING_SCENE_NAME);
}
