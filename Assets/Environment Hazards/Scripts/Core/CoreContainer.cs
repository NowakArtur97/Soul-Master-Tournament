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

    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer
    {
        get => GenericUtil<SpriteRenderer>.GetOrDefault(_spriteRenderer, transform.parent.name);
        private set => _spriteRenderer = value;
    }

    private Sounds _sounds;

    public Sounds Sounds
    {
        get => GenericUtil<Sounds>.GetOrDefault(_sounds, transform.parent.name);
        private set => _sounds = value;
    }

    private void Awake()
    {
        Animation = GetComponentInChildren<Animation>();
        AnimationToStateMachine = GetComponentInChildren<AnimationToStateMachine>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Sounds = GetComponentInChildren<Sounds>();
    }
}
