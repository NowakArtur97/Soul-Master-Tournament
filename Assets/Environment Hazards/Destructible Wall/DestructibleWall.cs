using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    private const string ABILITY_TAG = "Soul Ability";

    [SerializeField]
    private Sprite[] _sprites;

    private void Awake() => GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length - 1)];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ABILITY_TAG))
        {
            Destroy(gameObject);
        }
    }
}
