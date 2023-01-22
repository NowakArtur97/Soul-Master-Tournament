using UnityEngine;

[CreateAssetMenu(fileName = "_MoveAndDealDamageOnContactState", menuName = "Data/Environment Hazard Move And Deal Damage On Contact State")]
public class D_EnvironmentHazardMoveAndDealDamageOnContactState : D_EnvironmentHazardDealDamageOnContactState
{
    public LayerMask whatIsFloor;
    public float movementSpeed = 5.0f;
    public float floorCheckDistance = 0.39f;
    public float minMoveTime = 4.0f;
    public float maxMoveTime = 7.0f;
}
