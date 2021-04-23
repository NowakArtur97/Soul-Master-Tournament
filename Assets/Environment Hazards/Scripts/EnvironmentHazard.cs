using System.Collections;
using UnityEngine;

public abstract class EnvironmentHazard : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string ACTIVE_ANIMATION_BOOL_NAME = "active";

    private EnvironmentHazardAnimationToComponent _environmentHazardAnimationToComponent;
    protected GameObject AliveGameObject { get; private set; }
    private Animator _myAnimator;
    private Rigidbody2D _myRigidbody2D;

    protected Coroutine IdleCoroutine;
    protected enum Status { TRIGGERED, ACTIVE, FINISHED, EMPTY }

    protected Status CurrentStatus;

    private void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        _myAnimator = AliveGameObject.GetComponent<Animator>();
        _myRigidbody2D = AliveGameObject.GetComponent<Rigidbody2D>();
        _environmentHazardAnimationToComponent = AliveGameObject.GetComponent<EnvironmentHazardAnimationToComponent>();

        if (_environmentHazardAnimationToComponent)
        {
            _environmentHazardAnimationToComponent.EnvironmentHazard = this;
        }

        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, true);

        CurrentStatus = Status.FINISHED;
    }

    protected abstract void UseEnvironmentHazard();

    protected virtual void TriggerEnvironmentHazard() => SetIsAnimationActive(true);

    protected virtual void FinishUsingEnvironmentHazard() => SetIsAnimationActive(false);

    protected virtual IEnumerator WaitBeforeAction(float timeToWait, Status statusAfterWaiting)
    {
        CurrentStatus = Status.EMPTY;

        yield return new WaitForSeconds(timeToWait);

        CurrentStatus = statusAfterWaiting;

        StopCoroutine(IdleCoroutine);
    }

    protected void SetIsAnimationActive(bool isActive)
    {
        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME, !isActive);
        _myAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME, isActive);
    }

    public virtual void StartUsingEnvironmentHazardTrigger() => CurrentStatus = Status.ACTIVE;

    public virtual void StopUsingEnvironmentHazardTrigger() => CurrentStatus = Status.FINISHED;

    protected void SetVelocityZero() => _myRigidbody2D.velocity = Vector2.zero;

    protected void SetVelocity(float speed, int direction) => _myRigidbody2D.velocity = new Vector2(direction * speed, 0);

    protected bool CheckIfPlayerInMinAgro(out GameObject toInteract, LayerMask[] whatIsInteractable)
    {
        foreach (LayerMask damagableLayerMask in whatIsInteractable)
        {
            Collider2D collision = Physics2D.OverlapBox(AliveGameObject.transform.position, Vector2.one, 0f, damagableLayerMask);
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
