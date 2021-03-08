public class EvilEyeSoul : SoulWithLinearAbility
{
    protected const string ABILITY_ANIMATION_BOOL_NAME = "ability";
    protected const string MIDDLE_POS_ANIMATION_BOOL_NAME = "middle";
    protected const string END_POS_ANIMATION_BOOL_NAME = "end";

    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            Destroy(gameObject);
        }
        else if (ShouldStartUsingAbility)
        {
            ShouldStartUsingAbility = false;
            StartUsingAbility();
        }
        else if (IsUsingAbility)
        {
            IsUsingAbility = false;
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
        }
    }

    protected override string GetAnimationBoolName(int range) =>
        range == AbilityRange
        || CheckIfTouchingWall(range + 1, AbilityDirection, SoulStats.notAfectedLayerMasks) ? END_POS_ANIMATION_BOOL_NAME : MIDDLE_POS_ANIMATION_BOOL_NAME;
}
