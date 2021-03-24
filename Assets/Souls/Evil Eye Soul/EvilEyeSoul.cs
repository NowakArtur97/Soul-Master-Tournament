public class EvilEyeSoul : SoulWithLinearAbility
{
    private const string ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME = "middle";
    private const string ABILITY_END_POS_ANIMATION_BOOL_NAME = "end";

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
        }
        else if (IsUsingAbility)
        {
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
        }
    }

    protected override string GetAnimationBoolName() =>
        AbilityRange == AbilityMaxRange || CheckIfTouchingWall(AbilityRange + 1, AbilityDirection, SoulStats.notAfectedLayerMasks)
        ? ABILITY_END_POS_ANIMATION_BOOL_NAME : ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME;
}
