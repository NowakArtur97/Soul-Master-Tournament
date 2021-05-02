using UnityEngine;

public class WaterShield : SoulAbility
{
    //TODO: Ice and Water Soul Ability: Refactor 
    [SerializeField]
    private string _abilityTag;
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
        _player = AliveGameObject.transform.parent.GetComponentInParent<Player>();
        _protectedStatus = new ProtectedStatus(_activeShieldTime);
        _player.PlayerStatusesManager.AddStatus(_protectedStatus);
    }

    protected override void Update()
    {
        base.Update();

        if (Time.time >= StartTime + _activeShieldTime)
        {
            HasFinished = true;

            _player.SetProtectedState(false);
            _player.PlayerStatusesManager.RemoveStatus(_protectedStatus);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name);
        if (collision.gameObject.CompareTag(_abilityTag))
        {
            _shieldHealth--;

            if (_shieldHealth <= 0)
            {
                HasFinished = true;
            }
        }
    }

    public override void FinishTrigger() { }
}
