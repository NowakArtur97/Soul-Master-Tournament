using System.Collections;
using UnityEngine;

public abstract class EnvironmentHazard : MonoBehaviour
{
    private const string ALIVE_GAME_OBJECT_NAME = "Alive";

    private const string IDLE_ANIMATION_BOOL_NAME = "idle";
    private const string ACTIVE_ANIMATION_BOOL_NAME = "active";
    private const string ON_WALL_ANIMATION_BOOL_MODIFIER = "-onWall";
    private const string ACTIVE_CLIP_TITLE = "_Active";
    private const string ACTIVED_CLIP_TITLE = "_Actived";

    [SerializeField]
    protected bool CanBeOnWall = false;
    [SerializeField]
    private string _environmentHazardName;
    [SerializeField]
    private float _timeToWaitAfterInstantiating = 0;

    private EnvironmentHazardAnimationToComponent _environmentHazardAnimationToComponent;
    protected GameObject AliveGameObject { get; private set; }
    private Animator _myAnimator;
    private Rigidbody2D _myRigidbody2D;

    protected Coroutine IdleCoroutine;
    protected enum Status { TRIGGERED, ACTIVE, FINISHED, EMPTY }

    protected Status CurrentStatus;

    protected virtual void Awake()
    {
        AliveGameObject = transform.Find(ALIVE_GAME_OBJECT_NAME).gameObject;
        _myAnimator = AliveGameObject.GetComponent<Animator>();
        _myRigidbody2D = AliveGameObject.GetComponent<Rigidbody2D>();
        _environmentHazardAnimationToComponent = AliveGameObject.GetComponent<EnvironmentHazardAnimationToComponent>();

        if (_environmentHazardAnimationToComponent)
        {
            _environmentHazardAnimationToComponent.EnvironmentHazard = this;
        }


        SetIsAnimationActive(false);

        _environmentHazardName = _environmentHazardName.Equals("") ? GetType().Name : _environmentHazardName;

        CurrentStatus = Status.EMPTY;

        StartCoroutine(WaitAfterInstantiating());
    }

    protected abstract void UseEnvironmentHazard();

    protected virtual void TriggerEnvironmentHazard() => SetIsAnimationActive(true);

    protected virtual void FinishUsingEnvironmentHazard() => SetIsAnimationActive(false);

    private IEnumerator WaitAfterInstantiating()
    {
        yield return new WaitForSeconds(_timeToWaitAfterInstantiating);

        CurrentStatus = Status.FINISHED;
    }

    protected virtual IEnumerator WaitBeforeAction(float timeToWait, Status statusAfterWaiting)
    {
        CurrentStatus = Status.EMPTY;

        yield return new WaitForSeconds(timeToWait);

        CurrentStatus = statusAfterWaiting;

        StopCoroutine(IdleCoroutine);
    }

    protected void SetIsAnimationActive(bool isActive)
    {
        if (_myAnimator == null)
        {
            return;
        }

        string onWallModifier = "";
        if (CanBeOnWall && IsRotated())
        {
            onWallModifier += ON_WALL_ANIMATION_BOOL_MODIFIER;
        }

        _myAnimator.SetBool(IDLE_ANIMATION_BOOL_NAME + onWallModifier, !isActive);
        _myAnimator.SetBool(ACTIVE_ANIMATION_BOOL_NAME + onWallModifier, isActive);
    }

    public void StartUsingEnvironmentHazardTrigger() => CurrentStatus = Status.ACTIVE;

    public void StopUsingEnvironmentHazardTrigger() => CurrentStatus = Status.FINISHED;

    public void PlayActiveSoundTrigger() => AudioManager.Instance.Play(_environmentHazardName + ACTIVE_CLIP_TITLE);

    public void PlayActivedSoundTrigger() => AudioManager.Instance.Play(_environmentHazardName + ACTIVED_CLIP_TITLE);

    protected void SetVelocityZero() => _myRigidbody2D.velocity = Vector2.zero;

    protected void SetVelocity(float speed, Vector2 direction) => _myRigidbody2D.velocity = direction * speed;

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

    private bool IsRotated() => transform.rotation != Quaternion.Euler(0, 0, 0);

    protected void SetOffsetFromWall(Vector2[] offsetsFromWall)
    {
        if (CanBeOnWall)
        {
            if (transform.rotation.eulerAngles.z == 0)
            {
                AliveGameObject.transform.localPosition += (Vector3)offsetsFromWall[0];
            }
            else if (transform.rotation.eulerAngles.z == 180)
            {
                AliveGameObject.transform.localPosition += (Vector3)offsetsFromWall[1];
            }
            else if (transform.rotation.eulerAngles.z == 90)
            {
                AliveGameObject.transform.localPosition += (Vector3)offsetsFromWall[2];
            }
            else
            {
                AliveGameObject.transform.localPosition += (Vector3)offsetsFromWall[3];
            }
        }
    }
}
