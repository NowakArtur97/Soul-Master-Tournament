public class IceSoul : SoulWithLinearAbility
{
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
            MyAnimator.SetBool("ability", true);
        }
    }

    protected override string GetAnimationBoolName(int range) => "create";
}
