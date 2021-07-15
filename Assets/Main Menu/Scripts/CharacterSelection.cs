using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private List<int> _characterIndexes;

    private void Awake()
    {
        _characterIndexes = new List<int>();
    }

    public void SelectCharacter(int index)
    {
        if (_characterIndexes.Contains(index))
        {
            _characterIndexes.Remove(index);
        }
        else
        {
            _characterIndexes.Add(index);
        }
    }

    public List<int> GetChosenCharactersIndexes() => _characterIndexes;
}
