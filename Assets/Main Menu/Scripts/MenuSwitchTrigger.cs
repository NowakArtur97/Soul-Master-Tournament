using UnityEngine;

public class MenuSwitchTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _firstMenu;
    [SerializeField]
    private GameObject _secondMenu;

    public void SwitchMenusTrigger()
    {
        _firstMenu.SetActive(!_firstMenu.activeInHierarchy);
        _secondMenu.SetActive(!_secondMenu.activeInHierarchy);
    }
}
