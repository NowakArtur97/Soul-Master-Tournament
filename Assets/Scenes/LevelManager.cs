using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void PlayLevel()
    {
        // TODO: LevelManager: Load chosen level
        SceneManager.LoadScene("Level 1");
    }
}
