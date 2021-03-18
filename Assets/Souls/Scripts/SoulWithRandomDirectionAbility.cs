using System.Collections.Generic;
using UnityEngine;

public abstract class SoulWithRandomDirectionAbility : Soul
{
    private IList<Vector2> _takenPositions;
    private Vector2 _soulPosition;

    protected override void StartUsingAbility()
    {
        _takenPositions = new List<Vector2>();

        base.StartUsingAbility();
    }

    protected override void UseAbility()
    {
        int range = GetRandomRange(AbilityMaxRange);

        _soulPosition = GetSoulPosition();

        while (_takenPositions.Contains(_soulPosition) && CheckIfTouchingWall(0, _soulPosition, SoulStats.notAfectedLayerMasks))
        {
            _soulPosition = GetSoulPosition();
        }

        _takenPositions.Add(_soulPosition);

        SoulAbility ability = Instantiate(SoulAbility, _soulPosition, GetSoulRotation());

        ability.GetComponentInChildren<Animator>().SetBool(GetAnimationBoolName(), true);
    }

    protected override Vector2 GetSoulPosition() => new Vector2(Mathf.FloorToInt(AliveGameObject.transform.position.x) + (AbilityDirection.x * AbilityMaxRange),
        Mathf.FloorToInt(_soulPosition.y) + (AbilityDirection.y * AbilityMaxRange));

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);

    private int GetRandomRange(int maxRange) => Random.Range(1, maxRange);
}
