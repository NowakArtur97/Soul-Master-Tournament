using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private const string RANDOM_LEVEL_NAME = "Random";

    [SerializeField]
    private Image _levelImage;
    [SerializeField]
    private Button _previousLevelButton;
    [SerializeField]
    private Button _nextLevelButton;
    [SerializeField]
    private TextMeshProUGUI _levelNameText;
    [SerializeField]
    private TextMeshProUGUI _randomLevelQuestionMarkText;
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

        D_Level chosenLevel = _levelsData[_currentLevel];
        _levelImage.sprite = chosenLevel.image;

        _levelNameText.text = chosenLevel.name;

        bool isRandomLevel = RANDOM_LEVEL_NAME.Equals(chosenLevel.name);
        _randomLevelQuestionMarkText.gameObject.SetActive(isRandomLevel);
    }

    public void ChangeLevel(int change)
    {
        _currentLevel += change;
        SelectLevel(_currentLevel);
    }

    public string GetSelectedLevelName() => _levelsData[_currentLevel].name;
}
