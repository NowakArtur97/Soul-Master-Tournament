using UnityEngine;

public abstract class SoulWithPlayerBuff : Soul
{
    private GameObject _playerAliveGameObject;

    private void Start()
    {
        _playerAliveGameObject = FindObjectOfType<Player>().transform.Find("Alive").gameObject;
    }

    protected override void Update()
    {
        base.Update();

        LockToPlayerPosition();
    }

    protected override void UseAbility()
    {
        SoulAbility ability = Instantiate(SoulAbility, GetSoulPosition(), GetSoulRotation());

        ability.GetComponentInChildren<Animator>()?.SetBool(GetAnimationBoolName(), true);

        ability.transform.parent = _playerAliveGameObject.transform;
    }

    protected override Vector2 GetSoulPosition() => _playerAliveGameObject.transform.position;

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);

    private void LockToPlayerPosition() =>
        transform.position = new Vector2(_playerAliveGameObject.transform.position.x, _playerAliveGameObject.transform.position.y) + SoulStats.startPositionOffset;
}
