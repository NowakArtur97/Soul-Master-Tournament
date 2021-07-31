using UnityEngine;

public class WinnerManager : MonoBehaviour
{
    private static WinnerManager _instance;
    public Sprite WinnerSprite { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetWinnerSprite(Player player) => WinnerSprite = player.GetComponentInChildren<SpriteRenderer>().sprite;
}
