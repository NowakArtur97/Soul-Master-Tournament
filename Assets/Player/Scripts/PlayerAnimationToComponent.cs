using UnityEngine;

public class PlayerAnimationToComponent : MonoBehaviour
{
    public Player Player;

    private void DestroyShieldTrigger() => Player.DestroyShieldTrigger();

    private void SummonedTrigger() => Player.SummonedTrigger();

    private void DeathTrigger() => Player.DeathTrigger();
}
