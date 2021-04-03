using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    [SerializeField]
    private D_ProjectileStats _projectileStatsData;
    [SerializeField]
    private Transform _attackPosition;

    private GameObject _aliveGameObject;

    private Rigidbody2D _myRigidBody2d;
    private AttackDetails _attackDetails;

    private bool _hasHitGround;
    private Collider2D _groundHit;
    private Collider2D _damageHit;
    private float _damageRadius;
    private LayerMask[] _whatIsGround;
    private LayerMask[] _whatIsDamagable;
    private Vector2 _direction;

    private void Awake()
    {
        _aliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;

        _myRigidBody2d = _aliveGameObject.GetComponent<Rigidbody2D>();

        _myRigidBody2d.velocity = _direction * _projectileStatsData.speed;

        _attackDetails.damageAmount = _projectileStatsData.damage;
        _damageRadius = _projectileStatsData.damageRadius;
        _whatIsGround = _projectileStatsData.whatIsGround;
        _whatIsDamagable = _projectileStatsData.whatIsDamagable;
    }

    private void Update()
    {
        if (!_hasHitGround)
        {
            _damageHit = CheckIfIsTouching(_whatIsDamagable);
            _groundHit = CheckIfIsTouching(_whatIsGround);

            if (_damageHit)
            {
                _damageHit.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage(_attackDetails);
            }
            else if (_groundHit)
            {
                _hasHitGround = true;
            }
        }

        if (_damageHit || _groundHit)
        {
            Destroy(gameObject);
        }
    }

    private Collider2D CheckIfIsTouching(LayerMask[] layerMasks)
    {
        foreach (var layerMask in layerMasks)
        {
            Collider2D collider = Physics2D.OverlapCircle(_attackPosition.position, _damageRadius, layerMask);

            if (collider)
            {
                return collider;
            }
        }

        return null;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        Debug.Log(direction);
    }
}
