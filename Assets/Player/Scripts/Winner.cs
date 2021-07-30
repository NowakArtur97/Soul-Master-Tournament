using UnityEngine;

public class Winner : MonoBehaviour
{
    private void Start() => SetWinnerSprite();

    private void SetWinnerSprite() => GetComponent<SpriteRenderer>().sprite = FindObjectOfType<WinnerManager>().WinnerSprite;
}
