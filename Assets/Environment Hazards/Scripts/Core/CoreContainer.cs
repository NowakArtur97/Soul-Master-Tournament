using UnityEngine;

public class CoreContainer : MonoBehaviour
{
    private Animation _animation;

    public Animation Animation
    {
        get => GenericUtil<Animation>.GetOrDefault(_animation, transform.parent.name);
        private set => _animation = value;
    }

    private AnimationToStateMachine _animationToStateMachine;

    public AnimationToStateMachine AnimationToStateMachine
    {
        get => GenericUtil<AnimationToStateMachine>.GetOrDefault(_animationToStateMachine, transform.parent.name);
        private set => _animationToStateMachine = value;
    }

    private void Awake()
    {
        Animation = GetComponentInChildren<Animation>();
        AnimationToStateMachine = GetComponentInChildren<AnimationToStateMachine>();
    }
}
