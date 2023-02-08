using UnityEngine;

public class WinnerSpawner : MonoBehaviour
{
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";

    [SerializeField] private GameObject[] _winnerPlayerPrefabs;
    [SerializeField] private Vector2 _winnerPosition = new Vector2(0.06f, 0.78f);

    private void Start()
    {
        GameObject winner = Instantiate(_winnerPlayerPrefabs[FindObjectOfType<WinnerManager>().WinnerIndex], _winnerPosition,
            Quaternion.identity);
        winner.GetComponentInChildren<Animator>().SetBool(IDLE_ANIMATION_BOOL_NAME, true);
    }
}
