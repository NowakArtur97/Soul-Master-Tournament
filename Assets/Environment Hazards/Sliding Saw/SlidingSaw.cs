using UnityEngine;

public class SlidingSaw : EnvironmentHazardDamagingOnContact
{
    [SerializeField]
    private LayerMask _whatIsFloor;
    [SerializeField]
    private float _sawSpeed;
    [SerializeField]
    private float _floorCheckDistance = 1f;
    [SerializeField]
    private Transform _floorCheck;

    private int _movingDirection = 1;

    protected override void Update()
    {
        base.Update();

        if (CheckIfTouchingFloor())
        {
            ChangeDirection();
        }
    }

    protected override void IdleTimeIsOver()
    {
        base.IdleTimeIsOver();

        IsActive = true;

        SetVelocity();
    }

    protected override void UseEnvironmentHazard()
    {
        GameObject toDamage;
        Debug.Log(CheckIfTouchingFloor());

        if (CheckIfPlayerInMinAgro(out toDamage))
        {
            toDamage.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage(AttackDetails);
        }
    }

    private void ChangeDirection()
    {
        _movingDirection *= -1;
        SetVelocity();
    }

    private void SetVelocity() => MyRigidbody2D.velocity = new Vector2(_movingDirection * _sawSpeed, 0);

    private bool CheckIfTouchingFloor() => Physics2D.Raycast(_floorCheck.position, Vector2.right * _movingDirection, _floorCheckDistance, _whatIsFloor);
}
