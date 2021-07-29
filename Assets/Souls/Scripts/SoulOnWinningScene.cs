using System.Collections;
using UnityEngine;

public class SoulOnWinningScene : MonoBehaviour
{
    protected const string SUMMON_ANIMATION_BOOL_NAME = "summon";

    [SerializeField]
    private GameObject[] _souls;
    [SerializeField]
    private float _minTimeToSummon = 0.5f;
    [SerializeField]
    private float _maxTimeToSummon = 1.5f;

    private void Start() => StartCoroutine(SummonCoroutine());

    private IEnumerator SummonCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeToSummon, _maxTimeToSummon));

        GetComponent<Animator>().SetBool(SUMMON_ANIMATION_BOOL_NAME, true);
    }
}
