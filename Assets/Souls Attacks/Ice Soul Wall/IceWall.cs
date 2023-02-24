using UnityEngine;

public class IceWall : SoulAbility
{
    [SerializeField]
    private int _wallDexterity;

    private readonly string ABILITY_TAG = "Soul Ability";
    private readonly string PLAYER_TAG = "Player";
    private int _wallHealth;
    private bool _canDealDamage;

    private Collider2D _myCollider2D;

    protected override void Awake()
    {
        base.Awake();

        _myCollider2D = AliveGameObject.GetComponent<Collider2D>();
    }

    private void Start() => _wallHealth = _wallDexterity;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ABILITY_TAG))
        {
            HandleDamageToWall();
        }
        else if (_canDealDamage && collision.gameObject.CompareTag(PLAYER_TAG))
        {
            DealDamageToPlayer(collision);
        }
    }

    public override void ActiveTrigger()
    {
        base.ActiveTrigger();

        _canDealDamage = true;
    }

    public override void FinishTrigger()
    {
        _canDealDamage = false;

        _myCollider2D.isTrigger = false;
    }

    private void HandleDamageToWall()
    {
        _wallHealth--;

        if (_wallHealth <= 0)
        {
            HasFinished = true;
        }
    }

    private void DealDamageToPlayer(Collider2D collision) =>
        collision.gameObject.transform.parent.gameObject.GetComponentInChildren<IDamagable>()?.Damage();
}
