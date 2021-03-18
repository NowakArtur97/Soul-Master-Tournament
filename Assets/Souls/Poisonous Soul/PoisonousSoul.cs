public class PoisonousSoul : SoulWithRandomDirectionAbility
{
    private const string ABILITY_ANIMATION_BOOL_NAME = "ability";
    private const string START_ANIMATION_BOOL_NAME = "start";

    protected override void Update()
    {
        base.Update();

        if (HasMaxAbilityTimeFinished)
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
            ShouldStartUsingAbility = true;
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
        }
    }

    public override void StartUsingAbilityTrigger() => IsUsingAbility = true;

    protected override string GetAnimationBoolName(int range) => START_ANIMATION_BOOL_NAME;
}
