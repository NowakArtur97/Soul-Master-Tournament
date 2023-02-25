public class WaterSoul : SoulWithPlayerBuff
{
    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string START_ABIlITY_ANIMATION_BOOL_NAME = "active";

    protected override void Update()
    {
        base.Update();

        if (HasUsedAbility)
        {
            MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, false);
            UnsummonSoul();
        }
        else if (ShouldStartUsingAbility)
        {
            if (Player.PlayerStatsManager.IsSpawning || Player == null)
            {
                StopFollowingPlayer = true;
                ResetAllAnimatorBoolVariables();
                HasUsedAbility = true;
            }
            else
            {
                ShouldStartUsingAbility = false;
                StartUsingAbility();
            }
        }
        else if (IsUsingAbility && MyAnimator.GetBool(ABILITY_ANIMATION_BOOL_NAME) != true)
        {
            if (Player.PlayerStatsManager.IsSpawning || Player == null)
            {
                StopFollowingPlayer = true;
                ResetAllAnimatorBoolVariables();
                HasUsedAbility = true;
            }
            else
            {
                MyAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, false);
                MyAnimator.SetBool(ABILITY_ANIMATION_BOOL_NAME, true);
                Player.SetProtectedState(true);
            }
        }
        else if (IsSummoned && !HasAppeared)
        {
            FinishSummoningSoul(IDLE_ANIMATION_BOOL_NAME);
        }
    }

    protected override bool IsPlayerAlreadyBuffed() => PlayerAliveGameObject.GetComponentInParent<Player>().IsProtected();

    protected override string GetAnimationBoolName() => START_ABIlITY_ANIMATION_BOOL_NAME;
}
