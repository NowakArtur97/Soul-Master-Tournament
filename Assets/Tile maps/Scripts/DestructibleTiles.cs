using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTiles : MonoBehaviour
{
    [SerializeField]
    private string _explosionTag;

    private Tilemap _myTilemap;
    private Vector3Int _tilePosition;

    private void Awake()
    {
        _myTilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_explosionTag))
        {
            _tilePosition = _myTilemap.WorldToCell(collision.transform.position);
            _myTilemap.SetTile(_tilePosition, null);
        }
    }
}
