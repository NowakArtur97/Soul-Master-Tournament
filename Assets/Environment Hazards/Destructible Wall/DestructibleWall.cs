using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    private const string ABILITY_TAG = "Soul Ability";

    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private GameObject _soulSpawner;
    [SerializeField]
    private float _chancesForSpawningSoulAfterDestroying = 25;

    private void Awake() => GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length - 1)];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ABILITY_TAG))
        {
            if (ShouldSpawnSoul())
            {
                _soulSpawner.GetComponent<SoulSpawner>().SpawnPickUp(new Vector2((int)transform.position.x, (int)transform.position.y));
            }
            Destroy(gameObject);
        }
    }

    private bool ShouldSpawnSoul() => Random.Range(0, 100) < _chancesForSpawningSoulAfterDestroying;
}
