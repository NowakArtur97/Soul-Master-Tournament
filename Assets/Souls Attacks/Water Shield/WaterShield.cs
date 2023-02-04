using UnityEngine;

public class WaterShield : SoulAbility
{
    private readonly string ACTIVE_ANIMATION_BOOL_NAME = "active";

    [SerializeField]
    private float _activeShieldTime = 10.0f;
    [SerializeField]
    private int _shieldDexterity = 1;

    private Player _player;
    private int _shieldHealth;
    private PlayerStatus _protectedStatus;
    private Animator _myAnimator;

    private void Start()
    {
        _shieldHealth = _shieldDexterity;

        _protectedStatus = new ProtectedStatus(_activeShieldTime);
        _myAnimator = AliveGameObject.GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsActive)
        {
            IsActive = false;
            ProtectPlayer();
        }

        if (HasTimeFinished())
        {
            DestroyShield();
        }
    }

    public void DealDamage()
    {
        _shieldHealth--;

        if (IsDestroyed())
        {
            DestroyShield();
        }
    }

    private void ProtectPlayer()
    {
        _player = AliveGameObject.transform.parent.GetComponentInParent<Player>();
        _player.AddStatus(_protectedStatus);
    }

    private void DestroyShield()
    {
        _player.SetProtectedState(false);
        _player.RemoveStatus(_protectedStatus);
        _player.DestroyShieldTrigger();
        _myAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME, false);
    }

    private bool HasTimeFinished() => Time.time >= StartTime + _activeShieldTime;

    private bool IsDestroyed() => _shieldHealth <= 0;
}
