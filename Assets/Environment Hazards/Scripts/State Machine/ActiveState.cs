public abstract class ActiveState : State
{
    public ActiveState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName) { }
}
