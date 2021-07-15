using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private Button[] _characterOptions;

    private List<int> _characterIndexes;
    [SerializeField]
    private Color _selectedColor = new Color(0f, 126f, 0f, 1f);
    [SerializeField]
    private Color _deselectedColor = new Color(106f, 0f, 0f, 1f);

    private void Awake() => _characterIndexes = new List<int>();

    private void Start() => DeselectAll();

    public void SelectCharacter(int index)
    {
        if (_characterIndexes.Contains(index))
        {
            _characterIndexes.Remove(index);
            _characterOptions[index].image.color = _deselectedColor;
        }
        else
        {
            _characterIndexes.Add(index);
            _characterOptions[index].image.color = _selectedColor;
        }
        Debug.Log(string.Join(";", _characterIndexes.ToArray()));
    }

    private void DeselectAll()
    {
        for (int index = 0; index < _characterOptions.Length; index++)
        {
            _characterOptions[index].image.color = _deselectedColor;
        }
    }

    public List<int> GetChosenCharactersIndexes() => _characterIndexes;
}
