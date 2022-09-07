using System.Collections.Generic;
using UnityEngine;

public abstract class SoulWithRandomDirectionAbility : Soul
{
    [SerializeField]
    private Vector2 _minPosition = new Vector2(-5, 4);
    [SerializeField]
    private Vector2 _maxPosition = new Vector2(12, -13);

    private IList<Vector2> _takenPositions;
    private Vector2 _soulPosition;

    protected override void StartUsingAbility()
    {
        _takenPositions = new List<Vector2>();

        base.StartUsingAbility();
    }

    protected override void UseAbility()
    {
        _soulPosition = GetSoulPosition();

        while (_takenPositions.Contains(_soulPosition) && CheckIfTouching(0, _soulPosition, SoulStats.notAfectedLayerMasks))
        {
            _soulPosition = GetSoulPosition();
        }

        _takenPositions.Add(_soulPosition);

        SoulAbility ability = Instantiate(SoulAbility, _soulPosition, GetSoulRotation());

        ability.GetComponentInChildren<Animator>().SetBool(GetAnimationBoolName(), true);
    }

    protected override Vector2 GetSoulPosition() => new Vector2((int)Random.Range(_minPosition.x, _maxPosition.x),
        (int)Random.Range(_minPosition.y, _maxPosition.y));

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);

    private int GetRandomRange(int maxRange) => Random.Range(1, maxRange);
}
