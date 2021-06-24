using UnityEngine;

public class CrumblingFloor : EnvironmentHazardActiveOnContact
{
    private readonly string CRUMBLE_SOUND_CLIP = "CrumblingFloor_Active";

    [SerializeField]
    private Sprite[] _floorSprites;

    private SpriteRenderer _mySpriteRenderer;
    private int _currentSpriteIndex;

    private Coroutine _crumblingCorouting;

    protected override void Awake()
    {
        base.Awake();

        _mySpriteRenderer = AliveGameObject.GetComponent<SpriteRenderer>();

        _currentSpriteIndex = 0;

        _mySpriteRenderer.sprite = _floorSprites[_currentSpriteIndex];
    }

    protected override void UseEnvironmentHazard()
    {
        _currentSpriteIndex++;

        if (IsCrumbled())
        {
            TimeBeforeActivation = 0;
        }
        else if (WasCrubled())
        {
            Damage();
        }
        else
        {
            AudioManager.Instance.Play(CRUMBLE_SOUND_CLIP);
            Crumble();
        }

        StopUsingEnvironmentHazardTrigger();
    }

    private void Damage()
    {
        if (ToInteract)
        {
            IDamagable toDamage = ToInteract.GetComponentInParent<IDamagable>();

            if (toDamage != null)
            {
                toDamage.Damage();
            }
        }
    }

    private void Crumble() => _mySpriteRenderer.sprite = _floorSprites[_currentSpriteIndex];

    protected override void TriggerEnvironmentHazard() => IdleCoroutine = StartCoroutine(WaitBeforeAction(TimeBeforeActivation, Status.ACTIVE));

    protected override void FinishUsingEnvironmentHazard()
    {
        if (IdleCoroutine != null)
        {
            StopCoroutine(IdleCoroutine);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CurrentStatus == Status.FINISHED)
        {
            CurrentStatus = Status.TRIGGERED;
            ToInteract = collision.gameObject;
        }
    }

    private bool IsCrumbled() => _currentSpriteIndex == _floorSprites.Length;

    private bool WasCrubled() => _currentSpriteIndex > _floorSprites.Length;
}
