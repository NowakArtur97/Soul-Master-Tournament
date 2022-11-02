using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public State CurrentState;

    public void AnimationFinishedTrigger() => CurrentState.AnimationFinishedTrigger();

    public void AnimationTrigger() => CurrentState.AnimationTrigger();
}