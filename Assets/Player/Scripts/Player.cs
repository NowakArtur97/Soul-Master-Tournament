using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    [SerializeField]
    private D_PlayerStats _playerStats;
    [SerializeField]
    private GameObject _basicSoul;

    private Vector2 _movementInput;
    private bool _bombPlacedInput;

    private Vector2 _workspace;
    private Vector2 _currentVelocity;
    private Vector2 _bombPosition;
    private int _facingDirection = 1;

    private GameObject _aliveGameObject;
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _myRigidbody2D;

    public PlayerStatsManager PlayerStatsManager { get; private set; }

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();

        _aliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        _myRigidbody2D = _aliveGameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementInput.Set(_inputHandler.NormalizedInputX, _inputHandler.NormalizedInputY);
        _bombPlacedInput = _inputHandler.BombPlaceInput;

        CheckIfShouldFlip();

        if (_bombPlacedInput)
        {
            _inputHandler.UseBombPlaceInput();
            PlaceBomb();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerStatsManager.CanMove)
        {
            SetVelocity(_playerStats.movementSpeed * _movementInput);
        }
        else if (Time.time >= PlayerStatsManager.ImmobilityStartTime + PlayerStatsManager.ImmobilityTime)
        {
            PlayerStatsManager.UnlockMovement();
        }
        // When Player is Immobilize
        else if (_currentVelocity != Vector2.zero)
        {
            SetVelocity(Vector2.zero);
        }
    }

    private void PlaceBomb()
    {
        _bombPosition = SetBombPosition();
        Instantiate(_basicSoul, _bombPosition, Quaternion.Euler(0, _facingDirection == 1 ? 0 : 180, 0));
    }

    public void Damage()
    {
        PlayerStatsManager.TakeDamage();

        if (PlayerStatsManager.IsPermamentDead)
        {
            Destroy(gameObject);
        }
    }

    private Vector2 SetBombPosition() => new Vector2(
        Mathf.FloorToInt(_aliveGameObject.transform.position.x),
        Mathf.FloorToInt(_aliveGameObject.transform.position.y));

    private void SetVelocity(Vector2 velocity)
    {
        _workspace.Set(velocity.x, velocity.y);
        _myRigidbody2D.velocity = _workspace;
        _currentVelocity = _workspace;
    }

    private void CheckIfShouldFlip()
    {
        if (ShouldFlip())
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingDirection *= -1;
        _aliveGameObject.transform.Rotate(0, 180, 0);
    }

    private bool ShouldFlip() => _movementInput.x != 0 && _facingDirection != _movementInput.x;

    public void CreateStatsManager(int id) => PlayerStatsManager = new PlayerStatsManager(_playerStats, id);
}
