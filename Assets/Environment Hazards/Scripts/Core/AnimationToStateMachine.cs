using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public State CurrentState;

    public void AnimationFinishedTrigger() => CurrentState.AnimationFinishedTrigger();

    public void AnimationTrigger() => CurrentState.AnimationTrigger();

    public void PlayActiveSoundTrigger() => CurrentState.PlayActiveSoundTrigger();

    public void PlayActivedSoundTrigger() => CurrentState.PlayActivedSoundTrigger();
}