using System.Collections;
using UnityEngine;

public class SlidingSaw : EnvironmentHazardActiveAfterTime
{
    [SerializeField]
    private LayerMask _whatIsRails;
    [SerializeField]
    private float _sawSpeed;
    [SerializeField]
    private float _railsCheckDistance = 0.39f;
    [SerializeField]
    private Transform _railsCheck;
    [SerializeField]
    private float _minMoveTime = 4;
    [SerializeField]
    private float _maxMoveTime = 7;

    private int _movingDirection = 1;
    private float _moveTime;
    private Coroutine _moveCoroutine;
    private bool isMoving = false;

    protected override void Awake()
    {
        base.Awake();

        _moveTime = Random.Range(_minMoveTime, _maxMoveTime);
    }

    protected override void UseEnvironmentHazard()
    {
        GameObject toDamage;

        if (CheckIfPlayerInMinAgro(out toDamage, EnvironmentHazardData.whatIsInteractable))
        {
            toDamage.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage();
        }

        if (!CheckIfTouchingRails())
        {
            ChangeDirection();
        }

        if (!isMoving)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        isMoving = true;

        SetVelocity(_sawSpeed, transform.right * _movingDirection);

        yield return new WaitForSeconds(_moveTime);

        SetVelocityZero();

        isMoving = false;

        StopUsingEnvironmentHazardTrigger();
    }

    private void ChangeDirection()
    {
        _movingDirection *= -1;
        SetVelocity(_sawSpeed, transform.right * _movingDirection);
    }

    private bool CheckIfTouchingRails() => Physics2D.Raycast(_railsCheck.position, transform.right * _movingDirection, _railsCheckDistance, _whatIsRails);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_railsCheck.position, new Vector2(_railsCheck.position.x + _movingDirection * _railsCheckDistance, _railsCheck.position.y));
    }
}
