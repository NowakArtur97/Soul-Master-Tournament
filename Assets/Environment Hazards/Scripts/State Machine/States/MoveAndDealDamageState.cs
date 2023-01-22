using UnityEngine;

public class MoveAndDealDamageState : ActiveState
{
    private D_EnvironmentHazardMoveAndDealDamageOnContactState _moveAndDealDamageOnContactStateData;
    private SlidingSawEntity _environmentHazardEntity;

    private float _moveTime;
    private int _movingDirection = 1;
    private bool _shouldChangeDirection;

    public MoveAndDealDamageState(SlidingSawEntity environmentHazardEntity, string animationBoolName,
       D_EnvironmentHazardMoveAndDealDamageOnContactState moveAndDealDamageOnContactStateData)
      : base(environmentHazardEntity, animationBoolName)
    {
        _moveAndDealDamageOnContactStateData = moveAndDealDamageOnContactStateData;
        _environmentHazardEntity = environmentHazardEntity;
    }

    public override void Enter()
    {
        base.Enter();

        _moveTime = Random.Range(_moveAndDealDamageOnContactStateData.minMoveTime, _moveAndDealDamageOnContactStateData.maxMoveTime);

        EnvironmentHazardEntity.CoreContainer.Sounds.PlayActiveSound();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        EnvironmentHazardEntity.transform.position +=
         _movingDirection * EnvironmentHazardEntity.transform.right * _moveAndDealDamageOnContactStateData.movementSpeed * Time.deltaTime;

        if (EnvironmentHazardEntity.ToInteract.Count > 0)
        {
            DamageAll(EnvironmentHazardEntity.ToInteract);
        }

        if (_shouldChangeDirection)
        {
            ChangeDirection();
        }

        if (Time.time >= StateStartTime + _moveTime)
        {
            EnvironmentHazardEntity.StateMachine.ChangeState(EnvironmentHazardEntity.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _shouldChangeDirection = !CheckIfTouchingObstacles();
    }

    public override void Exit()
    {
        base.Exit();

        EnvironmentHazardEntity.CoreContainer.Sounds.PauseActiveSound();
    }

    private void ChangeDirection() => _movingDirection *= -1;

    private bool CheckIfTouchingObstacles() => Physics2D.Raycast(
        _environmentHazardEntity.ObstacleCheck.position,
        _environmentHazardEntity.transform.right * _movingDirection,
        _moveAndDealDamageOnContactStateData.floorCheckDistance,
        _moveAndDealDamageOnContactStateData.whatIsFloor
        );
}
