using UnityEngine;

public abstract class SoulWithPlayerBuff : Soul
{
    protected GameObject PlayerAliveGameObject { get; private set; }

    private void Start() => PlayerAliveGameObject = Player.transform.Find("Alive").gameObject;

    protected override void Update()
    {
        base.Update();

        if (Player != null)
        {
            LockToPlayerPosition();
        }
    }

    protected override void UseAbility()
    {
        if (IsPlayerAlreadyBuffed())
        {
            return;
        }

        SoulAbility ability = Instantiate(SoulAbility, GetSoulPosition(), GetSoulRotation());

        ability.GetComponentInChildren<Animator>()?.SetBool(GetAnimationBoolName(), true);

        ability.transform.parent = PlayerAliveGameObject.transform;
    }

    protected abstract bool IsPlayerAlreadyBuffed();

    protected override Vector2 GetSoulPosition() => PlayerAliveGameObject.transform.position;

    protected override Quaternion GetSoulRotation() => Quaternion.Euler(0, 0, -90 * AbilityDirectionIndex);

    private void LockToPlayerPosition() =>
        transform.position = new Vector2(PlayerAliveGameObject.transform.position.x, PlayerAliveGameObject.transform.position.y)
        + SoulStats.startPositionOffset;
}
