using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisablePlayButton : MonoBehaviour
{
    [SerializeField]
    private Button _playButton;

    private CharacterSelection _characterSelection;
    private List<int> _chosenCharactersIndexes;

    private void Start()
    {
        _characterSelection = FindObjectOfType<CharacterSelection>();
        _chosenCharactersIndexes = _characterSelection.CharacterIndexes;
    }

    private void Update()
    {
        if (_chosenCharactersIndexes.Count < 2)
        {
            _playButton.interactable = false;
            _playButton.image.color = new Color(1f, 1f, 1f, .5f);
        }
        else
        {
            _playButton.interactable = true;
            _playButton.image.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
