using UnityEngine;

public class WaterShield : SoulAbility
{
    [SerializeField]
    private float _activeShieldTime = 10.0f;
    [SerializeField]
    private int _shieldDexterity = 1;

    private Player _player;
    private int _shieldHealth;
    private PlayerStatus _protectedStatus;

    private void Start()
    {
        _shieldHealth = _shieldDexterity;

        _protectedStatus = new ProtectedStatus(_activeShieldTime);
        _player = AliveGameObject.transform.parent.GetComponentInParent<Player>();
        _player.AddStatus(_protectedStatus);
    }

    protected override void Update()
    {
        base.Update();

        if (HasTimeFinished())
        {
            HasFinished = true;
        }
    }

    public void DealDamage()
    {
        _shieldHealth--;

        if (IsDestroyed())
        {
            _player.SetProtectedState(false);
            _player.RemoveStatus(_protectedStatus);

            HasFinished = true;
        }
    }

    private bool HasTimeFinished() => Time.time >= StartTime + _activeShieldTime;

    private bool IsDestroyed() => _shieldHealth <= 0;
}
