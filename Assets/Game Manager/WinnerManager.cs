using UnityEngine;

public class WinnerManager : MonoBehaviour
{
    public static WinnerManager Instance { get; private set; }
    public Sprite WinnerSprite { get; private set; }

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

    public void SetWinnerSprite(Player player) => WinnerSprite = player.GetComponentInChildren<SpriteRenderer>().sprite;
}
