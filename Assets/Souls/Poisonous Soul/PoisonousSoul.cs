public class PoisonousSoul : SoulWithRandomDirectionAbility
{
    private const string START_ABILITY_ANIMATION_BOOL_NAME = "start";

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
        }
    }

    public override void StartUsingAbilityTrigger() => IsUsingAbility = true;

    protected override string GetAnimationBoolName() => START_ABILITY_ANIMATION_BOOL_NAME;
}
