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

    private GameObject _aliveGameObject;
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _myRigidbody2D;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();

        _aliveGameObject = transform.Find("Alive").gameObject;
        _myRigidbody2D = _aliveGameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementInput = _inputHandler.RawMovementInput;
        _bombPlacedInput = _inputHandler.BombPlaceInput;

        if (_bombPlacedInput)
        {
            _inputHandler.UseBombPlaceInput();
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
        return new Vector2(Mathf.RoundToInt(_aliveGameObject.transform.position.x), Mathf.RoundToInt(_aliveGameObject.transform.position.y));
    }

    private void SetVelocity(Vector2 velocity)
    {
        _workspace.Set(velocity.x, velocity.y);
        _myRigidbody2D.velocity = _workspace;
        _currentVelocity = _workspace;
    }
}
