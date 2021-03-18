using UnityEngine;

public class PoisonousWhirl : SoulAbility
{
    private const string ABILITY_START_ANIMATION_BOOL_NAME = "start";
    private const string ABILITY_FINISH_ANIMATION_BOOL_NAME = "finish";

    protected override void Update()
    {
        base.Update();

        if (Time.time >= StartTime + AbilityStats.activeTime)
        {
            MyAnimator.SetBool(ABILITY_START_ANIMATION_BOOL_NAME, false);
            MyAnimator.SetBool(ABILITY_FINISH_ANIMATION_BOOL_NAME, true);
        }
    }
}
