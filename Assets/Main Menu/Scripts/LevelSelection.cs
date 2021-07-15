using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    private Image _levelImage;
    [SerializeField]
    private Button _previousLevelButton;
    [SerializeField]
    private Button _nextLevelButton;
    [SerializeField]
    private D_Level[] _levelsData;

    private int _currentLevel;

    private void Awake()
    {
        _currentLevel = 0;

        SelectLevel(_currentLevel);
    }

    private void SelectLevel(int level)
    {
        _previousLevelButton.interactable = (level != 0);
        _nextLevelButton.interactable = (level < _levelsData.Length - 1);

        _levelImage.sprite = _levelsData[_currentLevel].image;
    }

    public void ChangeLevel(int change)
    {
        _currentLevel += change;
        SelectLevel(_currentLevel);
    }

    public string GetSelectedLevel() => _levelsData[_currentLevel].name;
}
