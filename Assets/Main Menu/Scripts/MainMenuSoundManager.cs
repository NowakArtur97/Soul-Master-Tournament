using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    public void PlayButtonHighlightedSound() => AudioManager.Instance.Play("Button_Highlight");
}
