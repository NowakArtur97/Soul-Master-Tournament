using UnityEngine;

public class Flamethrower : EnvironmentHazardActiveAfterTime
{
    [SerializeField]
    private Vector2[] _offsetsFromWall;

    private BoxCollider2D _myBoxCollider2D;

    protected override void Awake()
    {
        base.Awake();

        _myBoxCollider2D = AliveGameObject.GetComponent<BoxCollider2D>();

        SetOffsetFromWall(_offsetsFromWall);
    }

    protected override void UseEnvironmentHazard()
    {
        if (!_myBoxCollider2D.enabled)
        {
            _myBoxCollider2D.enabled = true;
        }
    }

    protected override void FinishUsingEnvironmentHazard()
    {
        _myBoxCollider2D.enabled = false;

        base.FinishUsingEnvironmentHazard();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (CurrentStatus == Status.ACTIVE)
        {
            collision.gameObject.transform.parent.GetComponent<IDamagable>()?.Damage();
        }
    }
}
