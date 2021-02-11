using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private D_PlayerStats _playerStats;

    private Vector2 _movementInput;
    private Vector2 _workspace;
    private Vector2 _currentVelocity;
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
    }

    private void FixedUpdate()
    {
        SetVelocity(_playerStats.movementSpeed * _movementInput);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _workspace.Set(velocity.x, velocity.y);
        _myRigidbody2D.velocity = _workspace;
        _currentVelocity = _workspace;
    }
}
