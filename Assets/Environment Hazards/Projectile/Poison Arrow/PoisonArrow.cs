using UnityEngine;

public class PoisonArrow : Projectile
{
    [SerializeField]
    private float _timeBeingPoisoned = 3.0f;

    protected override void ApplyProjectileEffect()
    {
        PlayerStatus reversedControlsStatus = new ReversedControlsStatus(_timeBeingPoisoned);
        DamageHit.gameObject.transform.parent.GetComponent<Player>()?.AddStatus(reversedControlsStatus);
    }
}
