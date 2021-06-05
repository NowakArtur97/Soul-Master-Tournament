using UnityEngine;

public class PlayerToChildTrigger : MonoBehaviour
{
    private Player _player;

    private void Start() => _player = transform.parent.GetComponent<Player>();

    private void OnTriggerEnter2D(Collider2D collision) => _player.OnTriggerEnter2D(collision);
}
