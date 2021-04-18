using System.Collections;
using UnityEngine;

public abstract class EnvironmentHazard : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string ACTIVE_ANIMATION_BOOL_NAME = "active";

    private EnvironmentHazardAnimationToComponent _environmentHazardAnimationToComponent;
    private GameObject _aliveGameObject;
    private Animator _myAnimator;
    protected Rigidbody2D MyRigidbody2D { get; private set; }

    protected Coroutine _idleCoroutine;
    protected enum Status { TRIGGERED, ACTIVE, FINISHED, EMPTY }

    protected Status CurrentStatus;

    private void Awake()
    {
        _aliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        _myAnimator = _aliveGameObject.GetComponent<Animator>();
        MyRigidbody2D = _aliveGameObject.GetComponent<Rigidbody2D>();
        _environmentHazardAnimationToComponent = _aliveGameObject.GetComponent<EnvironmentHazardAnimationToComponent>();

        if (_environmentHazardAnimationToComponent)
        {
            _environmentHazardAnimationToComponent.EnvironmentHazard = this;
        }

        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);

        CurrentStatus = Status.FINISHED;
    }

    protected abstract void UseEnvironmentHazard();
    protected abstract IEnumerator WaitBeforeAction();

    protected void SetIsAnimationActive(bool isActive)
    {
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isActive);
        _myAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME, isActive);
    }

    public virtual void StartUsingEnvironmentHazardTrigger() => CurrentStatus = Status.ACTIVE;

    public virtual void StopUsingEnvironmentHazardTrigger() => CurrentStatus = Status.FINISHED;

    protected bool CheckIfPlayerInMinAgro(out GameObject toInteract, LayerMask[] whatIsInteractable)
    {
        foreach (LayerMask damagableLayerMask in whatIsInteractable)
        {
            Collider2D collision = Physics2D.OverlapBox(_aliveGameObject.transform.position, Vector2.one, 0f, damagableLayerMask);
            if (collision)
            {
                toInteract = collision.gameObject;
                return true;
            }
        }

        toInteract = null;
        return false;
    }
}
