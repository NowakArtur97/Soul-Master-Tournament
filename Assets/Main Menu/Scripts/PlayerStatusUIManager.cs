using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _uiElements;

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
        int uiIndex = 0;

        characterIndexes.Sort();

        foreach (int index in characterIndexes)
        {
            GameObject uiElement = _uiElements[index];
            uiElement.SetActive(true);
            uiElement.GetComponent<RectTransform>().anchoredPosition = _uiPositions[uiIndex];

            if (uiElement.activeInHierarchy)
            {
                players[index]?.SetUI(_uiElements[index]);
            }

            uiIndex++;
        }
    }

    public void SetUIElements(Player[] players) => SetUIElements(CharacterSelection.Instance.CharacterIndexes, players);
}
