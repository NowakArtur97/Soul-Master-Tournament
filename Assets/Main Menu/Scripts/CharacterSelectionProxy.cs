using UnityEngine;

public class CharacterSelectionProxy : MonoBehaviour
{
    public void SelectCharacter(int index) => CharacterSelection.Instance.SelectCharacter(index);
}
