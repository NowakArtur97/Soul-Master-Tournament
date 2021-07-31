using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _uiElements;
    [SerializeField]
    private Vector2[] _anchoredPosition;

    private Vector2[] _uiPositions;

    private void Awake() => GetUIStartingPositions();

    private void GetUIStartingPositions()
    {
        int index = 0;
        _uiPositions = new Vector2[4];

        foreach (Transform childTransform in transform)
        {
            RectTransform component;
            if ((component = childTransform.GetComponent<RectTransform>()) != null)
            {
                _uiPositions[index] = component.anchoredPosition;
                index++;
            }
        }
    }

    private void SetUIElements(List<int> characterIndexes, Player[] players)
    {
        int positionIndex = 0;

        characterIndexes.Sort();

        foreach (int index in characterIndexes)
        {
            GameObject uiElement = _uiElements[index];
            uiElement.SetActive(true);

            SetElementPosition(positionIndex, uiElement);

            players[index].SetUI(uiElement);

            positionIndex++;
        }
    }

    private void SetElementPosition(int positionIndex, GameObject uiElement)
    {
        RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = _uiPositions[positionIndex];
        Vector2 anchorPosition = _anchoredPosition[positionIndex];
        rectTransform.anchorMin = anchorPosition;
        rectTransform.anchorMax = anchorPosition;
    }

    public void SetUIElements(Player[] players) => SetUIElements(CharacterSelection.Instance.CharacterIndexes, players);
}
