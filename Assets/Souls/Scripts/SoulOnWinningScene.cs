using System.Collections;
using UnityEngine;

public class SoulOnWinningScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _souls;
    [SerializeField]
    private float _minTimeToSummon = 0.5f;
    [SerializeField]
    private float _maxTimeToSummon = 1.5f;

    private void Start() => StartCoroutine(SummonCoroutine());

    private IEnumerator SummonCoroutine()
    {
        yield return new WaitForSeconds(GetRandomTimeToSummonSoul());

        Instantiate(GetRandomSoul(), transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private float GetRandomTimeToSummonSoul() => Random.Range(_minTimeToSummon, _maxTimeToSummon);

    private GameObject GetRandomSoul() => _souls[Random.Range(0, _souls.Length)];
}
