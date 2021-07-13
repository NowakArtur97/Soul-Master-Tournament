using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _optionsMenu;

    public void ShowOptionsMenuTrigger()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void HideOptionsMenuTrigger()
    {
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }
}
