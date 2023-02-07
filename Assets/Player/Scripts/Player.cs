using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string MOVE_ANIMATION_BOOL_NAME = "move";
    private const string DIRECTION_ANIMATION_FLOAT_NAME = "directionX";
    private const string PROTECTED_ANIMATION_BOOL_NAME = "protect";
    private const string SUMMON_ANIMATION_BOOL_NAME = "summon";
    private const string DEATH_ANIMATION_BOOL_NAME = "dead";
    private const string ABILITY_TAG = "Soul Ability";
    private const string PICK_UP_TAG = "Pick Up";

    [SerializeField] private const int DEFAULT_NUMBER_OF_SOULS = 1;

    [SerializeField] private D_PlayerStats _playerStats;
    [SerializeField] private GameObject _basicSoul;
    [SerializeField] private Transform _bombTransformPosition;
    [SerializeField] private GameObject _poisonedStatus;

    [SerializeField] private float _timeBetweenDamages = 0.5f;
    private float _lastDamageTime;
    private bool _isBeingSummoned;

    [SerializeField] private string _playerDeathSound = "Player_Death";

    private Vector2 _movementInput;
    private bool _bombPlacedInput;

    private Vector2 _workspace;
    private Vector2 _currentVelocity;
    private Vector2 _bombPosition;
    private int _facingDirection = 1;
    private float _lastXValue = 1;

    private GameObject _aliveGameObject;
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _myRigidbody2D;
    private Animator _myAnimator;

    public PlayerStatsManager PlayerStatsManager { get; private set; }
    public PlayerStatusesManager _playerStatusesManager { get; private set; }
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
        _myAnimator.SetFloat(DIRECTION_ANIMATION_FLOAT_NAME, _lastXValue);

        _playerStatusesManager = new PlayerStatusesManager();
        _playerSoulsManager = new PlayerSoulsManager();
        _playerSoulsManager.ChangeBaseSoul(_basicSoul, _playerStats.startingNumberOfSouls);

        _playerStatusesManager.LockMovement();
        _myAnimator.SetBool(SUMMON_ANIMATION_BOOL_NAME, true);

        _playerAnimationToComponent = _aliveGameObject.GetComponent<PlayerAnimationToComponent>();
        _playerAnimationToComponent.Player = this;

        _lastDamageTime = 0;

        _poisonedStatus.SetActive(false);
    }

    private void Update()
    {
        if (_inputHandler.InputX != 0)
        {
            _lastXValue = _movementInput.x;
        }
        _movementInput.Set(_inputHandler.InputX, _inputHandler.InputY);
        _bombPlacedInput = _inputHandler.BombPlaceInput;

        HandleSummoning();

        if (_playerStatusesManager.HasAnyStatusActive())
        {
            _playerStatusesManager.CheckStatuses();
        }
        if (_poisonedStatus.activeInHierarchy && !_playerStatusesManager.HasReversedControls)
        {
            DisablePoisonedStatus();
        }

        SetupMovementAnimations();
    }

    private void FixedUpdate()
    {
        if (_playerStatusesManager.CanMove)
        {
            SetVelocity(_playerStatusesManager.HasReversedControls
             ? _playerStats.movementSpeed * _movementInput * -1
                : _playerStats.movementSpeed * _movementInput);
        }
        else if (IsNotMoving) // When Player is Immobilized
        {
            SetVelocity(Vector2.zero);
        }
    }

    private void SetupMovementAnimations()
    {
        if (PlayerStatsManager.IsSpawning)
        {
            return;
        }

        if (_movementInput.x != 0 || _movementInput.y != 0)
        {
            PlayMoveAnimation(true, _inputHandler.InputX);
            CheckIfShouldFlipFacingDirection();
        }
        else
        {
            PlayMoveAnimation(false, _lastXValue);
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
        SetBombPosition();
        GameObject soul = Instantiate(_playerSoulsManager.CurrentSoul, _bombPosition,
            Quaternion.Euler(0, _facingDirection == 1 ? 0 : 180, 0));
        soul.GetComponent<Soul>().SetPlayer(this);
        _playerSoulsManager.ReduceNumberOfSoulsToPlace();
        _playerStatusUI.SetCurrentSoulImage(_playerSoulsManager.CurrentSoul.name);
        _playerStatusUI.SetNumberOfSouls(_playerSoulsManager.CurrentNumberOfSoulsToPlace);
    }

    private void Summoned()
    {
        _isBeingSummoned = false;
        PlaySummonAnimation(false);
        _playerStatusesManager.UnlockMovement();
        PlayerStatsManager.IsSpawning = false;
    }

    private void PlayMoveAnimation(bool isMoving, float directionX)
    {
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isMoving);
        _myAnimator.SetBool(MOVE_ANIMATION_BOOL_NAME, isMoving);
        SetDirectionAnimationVariable(directionX);
    }

    private void SetDirectionAnimationVariable(float directionX)
    {
        if (_playerStatusesManager.HasReversedControls)
        {
            _myAnimator.SetFloat(DIRECTION_ANIMATION_FLOAT_NAME, (directionX == 0 ? _lastXValue : directionX) * -1);
        }
        else
        {
            _myAnimator.SetFloat(DIRECTION_ANIMATION_FLOAT_NAME, directionX == 0 ? _lastXValue : directionX);
        }
    }

    private void PlaySummonAnimation(bool isSummoned)
    {
        _myAnimator.SetBool(SUMMON_ANIMATION_BOOL_NAME, isSummoned);
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isSummoned);
    }

    public void Damage()
    {
        if (CanBeDamaged() && !PlayerStatsManager.IsSpawning)
        {
            _lastDamageTime = Time.time;

            if (_playerStatusesManager.HasShield)
            {
                GetComponentInChildren<WaterShield>()?.DealDamage();
                return;
            }

            AudioManager.Instance.Play(_playerDeathSound);
            PlayerStatsManager.IsSpawning = true;
            DisablePoisonedStatus();
            PlayDeathAnimation(true);
            _playerStatusesManager.LockMovement();

            SetVelocity(Vector2.zero);
        }
    }

    private void PlayDeathAnimation(bool isDead)
    {
        PlayerStatsManager.IsSpawning = true;
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
        _myAnimator.SetBool(MOVE_ANIMATION_BOOL_NAME, false);
        _myAnimator.SetFloat(DIRECTION_ANIMATION_FLOAT_NAME, _lastXValue);
        _myAnimator.SetBool(DEATH_ANIMATION_BOOL_NAME, isDead);
    }

    private void TakeDamage()
    {
        if (_isBeingSummoned)
        {
            return;
        }
        _isBeingSummoned = true;
        PlayerStatsManager.TakeDamage();
        AddStatus(new ImmobilizedStatus(_timeBetweenDamages));
        _playerStatusUI.SetNumberOfLives(PlayerStatsManager.CurrentHealth);

        if (!PlayerStatsManager.IsPermamentDead)
        {
            PlayDeathAnimation(false);
            PlaySummonAnimation(true);
        }
    }

    private void PickUpSoul(Collider2D collision)
    {
        SoulPickUp pickUp = collision.GetComponent<SoulPickUp>();
        if (pickUp.WasPickedUp)
        {
            Destroy(collision.gameObject);
            return;
        }
        pickUp.WasPickedUp = true;
        D_SoulPickUp pickUpData = pickUp.SoulData;
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

    public void SetProtectedState(bool isInProtectedState) // TODO: Remove
    {
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isInProtectedState);
        _myAnimator.SetBool(PROTECTED_ANIMATION_BOOL_NAME, isInProtectedState);
    }

    private void SetBombPosition() => _bombPosition.Set(
        Mathf.FloorToInt(_bombTransformPosition.transform.position.x),
        Mathf.FloorToInt(_bombTransformPosition.transform.position.y));

    private void SetVelocity(Vector2 velocity)
    {
        _workspace.Set(velocity.x, velocity.y);
        _myRigidbody2D.velocity = _workspace;
        _currentVelocity = _workspace;
    }

    private void CheckIfShouldFlipFacingDirection()
    {
        if (ShouldFlip())
        {
            Flip();
        }
    }

    private void Flip() => _facingDirection *= -1;

    private bool ShouldFlip() => _facingDirection != _movementInput.x;

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

    public void SummonedTrigger() => Summoned();

    public void DeathTrigger() => TakeDamage();

    private bool IsNotMoving => _currentVelocity != Vector2.zero;

    private bool CanBeDamaged() => _lastDamageTime + _timeBetweenDamages <= Time.time;

    public void SetColorForAnimation(string color) => _myAnimator.SetBool(color, true); // TODO: Remove

    public bool IsProtected() => _playerStatusesManager.HasShield;

    public void EnablePoisonedStatus() => _poisonedStatus.SetActive(true);

    public void DisablePoisonedStatus() => _poisonedStatus.SetActive(false);
}
