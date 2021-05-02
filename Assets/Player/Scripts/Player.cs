using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string PROTECTED_ANIMATION_BOOL_NAME = "protect";

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
    private Animator _myAnimator;

    public PlayerStatsManager PlayerStatsManager { get; private set; }
    private PlayerStatusesManager _playerStatusesManager;
    private PlayerSoulsManager _playerSoulsManager;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();

        _aliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        _myRigidbody2D = _aliveGameObject.GetComponent<Rigidbody2D>();
        _myAnimator = _aliveGameObject.GetComponent<Animator>();

        _playerStatusesManager = new PlayerStatusesManager();
        _playerSoulsManager = new PlayerSoulsManager(_playerStats);

        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);
    }

    private void Update()
    {
        _movementInput.Set(_inputHandler.NormalizedInputX, _inputHandler.NormalizedInputY);
        _bombPlacedInput = _inputHandler.BombPlaceInput;

        CheckIfShouldFlip();

        if (_bombPlacedInput)
        {
            if (_playerSoulsManager.CanPlaceSoul())
            {
                SummonSoul();
            }
            _inputHandler.UseBombPlaceInput();
        }

        if (_playerStatusesManager.HasAnyStatusActive())
        {
            _playerStatusesManager.CheckStatuses();
        }
    }

    private void FixedUpdate()
    {
        if (_playerStatusesManager.HasReversedControls)
        {
            _movementInput *= -1;
        }

        if (_playerStatusesManager.CanMove)
        {
            SetVelocity(_playerStats.movementSpeed * _movementInput);
        }
        // When Player is Immobilized
        else if (_currentVelocity != Vector2.zero)
        {
            SetVelocity(Vector2.zero);
        }
    }

    private void SummonSoul()
    {
        _bombPosition = SetBombPosition();
        GameObject soul = Instantiate(_basicSoul, _bombPosition, Quaternion.Euler(0, _facingDirection == 1 ? 0 : 180, 0));
        soul.GetComponent<Soul>().SetPlayer(this);
        _playerSoulsManager.ReduceNumberOfSoulsToPlace();
    }

    public void Damage()
    {
        if (_playerStatusesManager.HasShield)
        {
            // TODO: Player: Destroy shield
            return;
        }

        PlayerStatsManager.TakeDamage();

        if (PlayerStatsManager.IsPermamentDead)
        {
            Destroy(gameObject);
        }
    }

    public void SetProtectedState(bool isInProtectedState)
    {
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isInProtectedState);
        _myAnimator.SetBool(PROTECTED_ANIMATION_BOOL_NAME, isInProtectedState);
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

    public void LetPlacingSouls() => _playerSoulsManager.IncreaseNumberOfSoulsToPlace();

    public void AddStatus(PlayerStatus status) => _playerStatusesManager.AddStatus(status);

    public void RemoveStatus(PlayerStatus status) => _playerStatusesManager.RemoveStatus(status);
}
