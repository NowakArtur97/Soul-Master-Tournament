using UnityEngine;

public class SlidingSaw : EnvironmentHazardDamagingOnContact
{
    [SerializeField]
    private LayerMask _railsLayerMask;
    private float _sawSpeed;

    private int _movingDirection = 1;

    protected override void UseEnvironmentHazard()
    {
        GameObject toDamage;

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
        MyRigidbody2D.velocity.Set(_movingDirection * _sawSpeed, 0);
    }

    private bool CheckIfTouchingRails() => Physics2D.OverlapBox(AliveGameObject.transform.position, Vector2.one, 0f, _railsLayerMask);
}
