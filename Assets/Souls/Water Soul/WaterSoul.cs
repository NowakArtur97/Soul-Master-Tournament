public class WaterSoul : SoulWithPlayerBuff
{
    private const string START_ABILITY_ANIMATION_BOOL_NAME = "active";

    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            UnsummonSoul();
        }
        else if (ShouldStartUsingAbility)
        {
            ShouldStartUsingAbility = false;
            StartUsingAbility();
            HasUsedAbility = true;
        }
        else if (IsUsingAbility)
        {
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
        }
    }

    protected override string GetAnimationBoolName() => START_ABILITY_ANIMATION_BOOL_NAME;
}
