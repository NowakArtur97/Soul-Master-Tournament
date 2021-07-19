using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private Button[] _characterOptions;
    [SerializeField]
    private Color _selectedColor = new Color(0f, 126f, 0f, 1f);
    [SerializeField]
    private Color _deselectedColor = new Color(106f, 0f, 0f, 1f);

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

        CharacterIndexes = new List<int>();
    }

    private void Start() => DeselectAll();

    public void SelectCharacter(int index)
    {
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

    private void DeselectAll()
    {
        for (int index = 0; index < _characterOptions.Length; index++)
        {
            _characterOptions[index].image.color = _deselectedColor;
        }
    }
}