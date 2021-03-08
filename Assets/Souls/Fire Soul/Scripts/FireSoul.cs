public class FireSoul : SoulWithDirectionalAbility
{
    private const string ABILITY_ANIMATION_BOOL_NAME = "ability";
    private const string MIDDLE_POS_ANIMATION_BOOL_NAME = "middle";
    private const string END_POS_ANIMATION_BOOL_NAME = "end";

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

    protected override string GetAnimationBoolName(int range) => range != AbilityRange ? MIDDLE_POS_ANIMATION_BOOL_NAME : END_POS_ANIMATION_BOOL_NAME;
}
