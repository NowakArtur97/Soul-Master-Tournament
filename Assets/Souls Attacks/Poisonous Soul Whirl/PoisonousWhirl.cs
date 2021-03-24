using UnityEngine;

public class PoisonousWhirl : SoulAbility
{
    private const string ABILITY_START_ANIMATION_BOOL_NAME = "start";
    private const string ABILITY_ACTIVE_ANIMATION_BOOL_NAME = "active";
    private const string ABILITY_FINISH_ANIMATION_BOOL_NAME = "finish";

    private void Start() => MyAnimator.SetBool(ABILITY_START_ANIMATION_BOOL_NAME, true);

    protected override void Update()
    {
        base.Update();

        if (Time.time >= StartTime + AbilityStats.activeTime)
        {
            IsActive = false;
            MyAnimator.SetBool(ABILITY_ACTIVE_ANIMATION_BOOL_NAME, false);
            MyAnimator.SetBool(ABILITY_FINISH_ANIMATION_BOOL_NAME, true);
        }
        else if (IsActive)
        {
            MyAnimator.SetBool(ABILITY_START_ANIMATION_BOOL_NAME, false);
            MyAnimator.SetBool(ABILITY_ACTIVE_ANIMATION_BOOL_NAME, true);
        }
    }
}
