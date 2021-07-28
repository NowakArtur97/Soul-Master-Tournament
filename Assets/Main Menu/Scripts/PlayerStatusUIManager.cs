using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _uiElements;
    [SerializeField]
    private Vector2[] _uiPositions;

    private void SetUIElements(List<int> characterIndexes, Player[] players)
    {
        int uiIndex = 0;

        foreach (int index in characterIndexes)
        {
            GameObject uiElement = _uiElements[uiIndex];
            uiElement.SetActive(true);
            uiElement.GetComponent<RectTransform>().anchoredPosition = _uiPositions[uiIndex];

            if (uiElement.activeInHierarchy)
            {
                players[index]?.SetUI(_uiElements[uiIndex]);
            }

            uiIndex++;
        }
    }

    public void SetUIElements(Player[] players) => SetUIElements(CharacterSelection.Instance.CharacterIndexes, players);
}
