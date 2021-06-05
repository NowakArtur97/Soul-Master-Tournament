using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    [SerializeField]
    private string _abilityTag;

    [SerializeField]
    private Sprite[] _sprites;

    private void Awake() => GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length - 1)];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_abilityTag))
        {
            Destroy(gameObject);
        }
    }
}
