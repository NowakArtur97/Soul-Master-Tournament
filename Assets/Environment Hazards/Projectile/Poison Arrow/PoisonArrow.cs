using UnityEngine;

public class PoisonArrow : Projectile
{
    [SerializeField] private float _timeBeingPoisoned = 3.0f;

    protected override void ApplyProjectileEffect()
    {
        PlayerStatus reversedControlsStatus = new ReversedControlsStatus(_timeBeingPoisoned);
        Player player = DamageHit.gameObject.transform.parent.GetComponent<Player>();

        if (player == null)
        {
            return;
        }

        if (player.IsAlreadyProtected())
        {
            player.GetComponentInChildren<WaterShield>()?.DealDamage();
        }
        else
        {
            player.AddStatus(reversedControlsStatus);
        }
    }
}
