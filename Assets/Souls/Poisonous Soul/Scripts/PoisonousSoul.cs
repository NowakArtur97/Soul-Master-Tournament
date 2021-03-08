public class PoisonousSoul : SoulWithRandomDirectionAbility
{
    private const string START_ANIMATION_BOOL_NAME = "start";

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
            ShouldStartUsingAbility = true;
        }
    }

    protected override string GetAnimationBoolName(int range) => START_ANIMATION_BOOL_NAME;
}
