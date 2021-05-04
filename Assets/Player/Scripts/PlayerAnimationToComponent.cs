using UnityEngine;

public class PlayerAnimationToComponent : MonoBehaviour
{
    public Player Player;

    private void DestroyShieldTrigger() => Player.DestroyShieldTrigger();
}
