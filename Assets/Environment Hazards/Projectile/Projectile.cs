using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    [SerializeField]
    private D_ProjectileStats _projectileStatsData;
    [SerializeField]
    private Transform _attackPosition;

    private GameObject _aliveGameObject;

    private Rigidbody2D _myRigidBody2d;

    private bool _hasHitGround;
    protected Collider2D DamageHit { get; private set; }
    private Collider2D _groundHit;
    private float _damageRadius;
    private LayerMask[] _whatIsGround;
    private LayerMask[] _whatIsDamagable;

    private void Awake()
    {
        _aliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;

        _myRigidBody2d = _aliveGameObject.GetComponent<Rigidbody2D>();

        _myRigidBody2d.velocity = transform.right * _projectileStatsData.speed;

        _damageRadius = _projectileStatsData.damageRadius;
        _whatIsGround = _projectileStatsData.whatIsGround;
        _whatIsDamagable = _projectileStatsData.whatIsDamagable;
    }

    private void Update()
    {
        if (!_hasHitGround)
        {
            DamageHit = CheckIfIsTouching(_whatIsDamagable);
            _groundHit = CheckIfIsTouching(_whatIsGround);

            if (DamageHit)
            {
                ApplyProjectileEffect();
            }
            else if (_groundHit)
            {
                _hasHitGround = true;
            }
        }

        if (DamageHit || _groundHit)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyProjectileEffect();

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
}
