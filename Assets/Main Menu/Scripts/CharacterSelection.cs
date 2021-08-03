using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    private const string PLAYER_OPTION_NAME = "Player Options";

    [SerializeField]
    private Color _selectedColor = new Color(0f, 126f, 0f, 1f);
    [SerializeField]
    private Color _deselectedColor = new Color(106f, 0f, 0f, 1f);

    private Button[] _characterOptions;
    public List<int> CharacterIndexes { get; private set; }

    public static CharacterSelection Instance { get; private set; }

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

        ResetSelection();
    }

    private void ResetSelection()
    {
        CharacterIndexes = new List<int>();
        _characterOptions = FindPlayerSelectionOptions();
        DeselectAll();
    }

    private Button[] FindPlayerSelectionOptions() => _characterOptions =
        FindObjectsOfType<GameObject>(true)
            .Where(go => go.name.Contains(PLAYER_OPTION_NAME))
            .Select(go => go.GetComponent<Button>())
            .OrderBy(go => go.name)
            .ToArray();

    public void SelectCharacter(int index)
    {
        if (_characterOptions[0] == null)
        {
            ResetSelection();
        }

        if (CharacterIndexes.Contains(index))
        {
            CharacterIndexes.Remove(index);
            _characterOptions[index].image.color = _deselectedColor;
        }
        else
        {
            CharacterIndexes.Add(index);
            _characterOptions[index].image.color = _selectedColor;
        }
    }

    private void DeselectAll() => _characterOptions.ToList().ForEach(option => option.image.color = _deselectedColor);
}