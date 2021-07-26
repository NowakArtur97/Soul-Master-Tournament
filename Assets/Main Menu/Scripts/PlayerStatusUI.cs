using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _uiElements;
    [SerializeField]
    private Vector2[] _uiPositions;

    private void Start() => SetUIElementsPositions(CharacterSelection.Instance.CharacterIndexes);

    private void SetUIElementsPositions(List<int> characterIndexes)
    {
        int positionIndex = 0;
        foreach (int index in characterIndexes)
        {
            _uiElements[index].SetActive(true);
            _uiElements[index].GetComponent<RectTransform>().anchoredPosition = _uiPositions[positionIndex++];
        }
    }
}
