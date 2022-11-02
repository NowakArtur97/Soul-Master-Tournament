using UnityEngine;

public class IdleState : State
{
    public IdleState(EnvironmentHazardEntity environmentHazardEntity, string animationBoolName)
        : base(environmentHazardEntity, animationBoolName) { }

    public override void LogicUpdate()
    {
        if (StateStartTime + .5f <= Time.time && EnvironmentHazardEntity.ToInteract)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.ActiveState);
        }
    }
}
