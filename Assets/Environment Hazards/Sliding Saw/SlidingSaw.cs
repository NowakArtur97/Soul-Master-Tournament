using UnityEngine;

public class SlidingSaw : EnvironmentHazardDamagingOnContact
{
    [SerializeField]
    private LayerMask _whatIsRails;
    [SerializeField]
    private float _sawSpeed;
    [SerializeField]
    private float _railsCheckDistance = 0.39f;
    [SerializeField]
    private Transform _railsCheck;

    private int _movingDirection = 1;

    protected override void IdleTimeIsOver()
    {
        base.IdleTimeIsOver();

        IsActive = true;

        SetVelocity();
    }

    protected override void UseEnvironmentHazard()
    {
        GameObject toDamage;

        Debug.Log(!CheckIfTouchingRails());

        if (CheckIfPlayerInMinAgro(out toDamage))
        {
            toDamage.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage(AttackDetails);
        }

        if (!CheckIfTouchingRails())
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        _movingDirection *= -1;
        SetVelocity();
    }

    private void SetVelocity() => MyRigidbody2D.velocity = new Vector2(_movingDirection * _sawSpeed, 0);

    private bool CheckIfTouchingRails() => Physics2D.Raycast(_railsCheck.position, Vector2.right * _movingDirection, _railsCheckDistance, _whatIsRails);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_railsCheck.position, new Vector2(_railsCheck.position.x + _movingDirection * _railsCheckDistance, _railsCheck.position.y));
    }
}
