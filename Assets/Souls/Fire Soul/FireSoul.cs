public class FireSoul : SoulWithDirectionalAbility
{
    private const string ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME = "middle";
    private const string ABILITY_END_POS_ANIMATION_BOOL_NAME = "end";

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
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
        }
    }

    protected override string GetAnimationBoolName() => AbilityRange != AbilityMaxRange
        ? ABILITY_MIDDLE_POS_ANIMATION_BOOL_NAME : ABILITY_END_POS_ANIMATION_BOOL_NAME;
}
