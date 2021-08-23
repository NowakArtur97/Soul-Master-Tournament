using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string PROTECTED_ANIMATION_BOOL_NAME = "protect";
    private const string DEATH_ANIMATION_BOOL_NAME = "dead";
    private const string ABILITY_TAG = "Soul Ability";
    private const string PICK_UP_TAG = "Pick Up";
    [SerializeField]
    private const int DEFAULT_NUMBER_OF_SOULS = 1;

    [SerializeField]
    private D_PlayerStats _playerStats;
    [SerializeField]
    private GameObject _basicSoul;

    [SerializeField]
    private float _timeBetweenDamages = 0.5f;
    private float _lastDamageTime;

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
    private PlayerAnimationToComponent _playerAnimationToComponent;

    private GameObject _playerStatusUIGO;
    private PlayerStatusUI _playerStatusUI;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();

        _aliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        _myRigidbody2D = _aliveGameObject.GetComponent<Rigidbody2D>();
        _myAnimator = _aliveGameObject.GetComponent<Animator>();

        _playerStatusesManager = new PlayerStatusesManager();
        _playerSoulsManager = new PlayerSoulsManager();
        _playerSoulsManager.ChangeBaseSoul(_basicSoul, _playerStats.startingNumberOfSouls);

        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);

        _playerAnimationToComponent = _aliveGameObject.GetComponent<PlayerAnimationToComponent>();
        _playerAnimationToComponent.Player = this;

        _lastDamageTime = 0;
    }

    private void Update()
    {
        _movementInput.Set(_inputHandler.InputX, _inputHandler.InputY);
        _bombPlacedInput = _inputHandler.BombPlaceInput;

        CheckIfShouldFlip();

        HandleSummoning();

        if (_playerStatusesManager.HasAnyStatusActive())
        {
            _playerStatusesManager.CheckStatuses();
        }

        if (_playerStatusesManager.HasReversedControls)
        {
            _movementInput *= -1;
        }
    }

    private void FixedUpdate()
    {
        if (_playerStatusesManager.CanMove)
        {
            SetVelocity(_playerStats.movementSpeed * _movementInput);
        }
        // When Player is Immobilized
        else if (IsNotMoving)
        {
            SetVelocity(Vector2.zero);
        }
    }

    private void HandleSummoning()
    {
        if (_bombPlacedInput)
        {
            if (_playerSoulsManager.CanPlaceSoul())
            {
                SummonSoul();
            }

            _inputHandler.UseBombPlaceInput();
        }
    }

    private void SummonSoul()
    {
        _bombPosition = SetBombPosition();
        GameObject soul = Instantiate(_playerSoulsManager.CurrentSoul, _bombPosition, Quaternion.Euler(0, _facingDirection == 1 ? 0 : 180, 0));
        soul.GetComponent<Soul>().SetPlayer(this);
        _playerSoulsManager.ReduceNumberOfSoulsToPlace();
        _playerStatusUI.SetCurrentSoulImage(_playerSoulsManager.CurrentSoul.name);
        _playerStatusUI.SetNumberOfSouls(_playerSoulsManager.CurrentNumberOfSoulsToPlace);
    }

    public void Damage()
    {
        if (CanBeDamaged())
        {
            _lastDamageTime = Time.time;

            if (_playerStatusesManager.HasShield)
            {
                GetComponentInChildren<WaterShield>()?.DealDamage();
                return;
            }

            PlayDeathAnimation(true);
        }
    }

    private void PlayDeathAnimation(bool isDead)
    {
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isDead);
        _myAnimator.SetBool(DEATH_ANIMATION_BOOL_NAME, isDead);
    }

    private void TakeDamage()
    {
        PlayerStatsManager.TakeDamage();
        _playerStatusUI.SetNumberOfLives(PlayerStatsManager.CurrentHealth);
        PlayDeathAnimation(false);
    }

    private void PickUpSoul(Collider2D collision)
    {
        D_SoulPickUp pickUpData = collision.GetComponent<SoulPickUp>().SoulData;
        _playerSoulsManager.ChangeSoul(pickUpData.soul, pickUpData.numberOfUses);
        Destroy(collision.gameObject);
    }

    public void SetUI(GameObject ui)
    {
        _playerStatusUIGO = ui;
        _playerStatusUI = _playerStatusUIGO.GetComponent<PlayerStatusUI>();

        SetupPlayerUI();
    }

    private void SetupPlayerUI()
    {
        _playerStatusUI.SetCurrentSoulImage(_playerSoulsManager.CurrentSoul.name);
        _playerStatusUI.SetNumberOfLives(PlayerStatsManager.CurrentHealth);
        _playerStatusUI.SetNumberOfSouls(_playerSoulsManager.CurrentNumberOfSoulsToPlace);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ABILITY_TAG))
        {
            Damage();
        }
        else if (collision.gameObject.CompareTag(PICK_UP_TAG))
        {
            PickUpSoul(collision);
            _playerStatusUI.SetCurrentSoulImage(_playerSoulsManager.CurrentSoul.name);
            _playerStatusUI.SetNumberOfSouls(_playerSoulsManager.CurrentNumberOfSoulsToPlace);
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

    public void LetPlacingSouls()
    {
        _playerSoulsManager.IncreaseNumberOfSoulsToPlace();
        _playerStatusUI.SetCurrentSoulImage(_playerSoulsManager.CurrentSoul.name);
        _playerStatusUI.SetNumberOfSouls(_playerSoulsManager.CurrentNumberOfSoulsToPlace);
    }

    public void AddStatus(PlayerStatus status) => _playerStatusesManager.AddStatus(status);

    public void RemoveStatus(PlayerStatus status) => _playerStatusesManager.RemoveStatus(status);

    public void RemoveAllStatuses() => _playerStatusesManager.RemoveAllStatuses();

    public void DestroyShieldTrigger() => _playerStatusesManager.DectivateShield();

    public void DeathTrigger() => TakeDamage();

    private bool IsNotMoving => _currentVelocity != Vector2.zero;

    private bool CanBeDamaged() => _lastDamageTime + _timeBetweenDamages <= Time.time;

    public void SetColorForAnimation(string color) => _myAnimator.SetBool(color, true);
}
