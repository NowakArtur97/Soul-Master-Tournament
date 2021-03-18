public class IceSoul : SoulWithLinearAbility
{
    private const string ABILITY_ANIMATION_BOOL_NAME = "ability";
    private const string START_ABILITY_ANIMATION_BOOL_NAME = "create";

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

    protected override string GetAnimationBoolName() => START_ABILITY_ANIMATION_BOOL_NAME;
}
