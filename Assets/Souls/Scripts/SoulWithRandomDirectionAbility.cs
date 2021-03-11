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
        int range = GetRandomRange(AbilityRange);

        _soulPosition = GetSoulPosition(AbilityRange);

        while (_takenPositions.Contains(_soulPosition) && CheckIfTouchingWall(0, _soulPosition, SoulStats.notAfectedLayerMasks))
        {
            _soulPosition = GetSoulPosition(AbilityRange);
        }
        Debug.Log(_soulPosition);
        _takenPositions.Add(_soulPosition);

        SoulAbility ability = Instantiate(SoulAbility, _soulPosition, GetSoulRotation());

        ability.GetComponentInChildren<Animator>().SetBool(GetAnimationBoolName(range), true);
    }

    protected override Vector2 GetSoulPosition(int range) => new Vector2(Mathf.FloorToInt(AliveGameObject.transform.position.x) + (AbilityDirection.x * range),
        Mathf.FloorToInt(_soulPosition.y) + (AbilityDirection.y * range));

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);

    private int GetRandomRange(int maxRange) => Random.Range(1, maxRange);
}
