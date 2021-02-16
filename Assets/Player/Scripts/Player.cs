using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private D_PlayerStats _playerStats;
    [SerializeField]
    private GameObject _basicSoul;

    private Vector2 _movementInput;
    private bool _bombPlacedInput;

    private Vector2 _workspace;
    private Vector2 _currentVelocity;
    private Vector2 _bombPosition;
    private int _facingDirection;

    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _myRigidbody2D;

    private void Awake()
    {
        _myRigidbody2D = GetComponentInChildren<Rigidbody2D>();
        _inputHandler = GetComponent<PlayerInputHandler>();
    }


    private void Update()
    {
        _movementInput = _inputHandler.RawMovementInput;
        _bombPlacedInput = _inputHandler.BombPlacedInput;

        if (_bombPlacedInput)
        {
            PlaceBomb();
        }
    }

    private void FixedUpdate()
    {
        SetVelocity(_playerStats.movementSpeed * _movementInput);
    }

    private void PlaceBomb()
    {
        _bombPosition = SetBombPosition();
        Instantiate(_basicSoul, _bombPosition, Quaternion.identity);
    }

    private Vector2 SetBombPosition()
    {
        return new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    private void SetVelocity(Vector2 velocity)
    {
        _workspace.Set(velocity.x, velocity.y);
        _myRigidbody2D.velocity = _workspace;
        _currentVelocity = _workspace;
    }
}
