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
    private float _startActiveTime;
    private float _moveTime;

    private void Start() => _moveTime = Random.Range(_minMoveTime, _maxMoveTime);

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

        if (Time.time >= _startActiveTime + _moveTime)
        {
            StopUsingEnvironmentHazardTrigger();
            SetVelocityZero();
        }
    }

    private void ChangeDirection()
    {
        _movingDirection *= -1;
        SetVelocity(_sawSpeed, _movingDirection);
    }

    private bool CheckIfTouchingRails() => Physics2D.Raycast(_railsCheck.position, Vector2.right * _movingDirection, _railsCheckDistance, _whatIsRails);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_railsCheck.position, new Vector2(_railsCheck.position.x + _movingDirection * _railsCheckDistance, _railsCheck.position.y));
    }
}
