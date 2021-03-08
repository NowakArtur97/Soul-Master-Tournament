public class PoisonousSoul : SoulWithDirectionalAbility
{
    private const string START_ANIMATION_BOOL_NAME = "start";

    protected override string GetAnimationBoolName(int range) => START_ANIMATION_BOOL_NAME;
}
