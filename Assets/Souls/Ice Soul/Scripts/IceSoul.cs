public class IceSoul : SoulWithLinearAbility
{
    private const string START_ANIMATION_BOOL_NAME = "create";
    private const string ABILITY_ANIMATION_BOOL_NAME = "ability";

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

    protected override string GetAnimationBoolName(int range) => START_ANIMATION_BOOL_NAME;
}
