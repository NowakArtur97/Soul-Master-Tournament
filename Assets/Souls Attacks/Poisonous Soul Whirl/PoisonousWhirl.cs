using UnityEngine;

public class PoisonousWhirl : SoulAbility
{
    private const string ABILITY_START_ANIMATION_BOOL_NAME = "start";
    private const string ABILITY_ACTIVE_ANIMATION_BOOL_NAME = "active";
    private const string ABILITY_FINISH_ANIMATION_BOOL_NAME = "finish";

    [SerializeField]
    private float _activeTime = 10f;

    private Animator _myAnimator;
    private BoxCollider2D _myBoxCollider2D;

    private void Start()
    {
        _myBoxCollider2D = AliveGameObject.GetComponent<BoxCollider2D>();
        _myAnimator = AliveGameObject.GetComponent<Animator>();

        _myBoxCollider2D.enabled = false;
        _myAnimator.SetBool(ABILITY_START_ANIMATION_BOOL_NAME, true);
    }

    protected override void Update()
    {
        base.Update();

        if (Time.time >= StartTime + _activeTime && IsActive)
        {
            IsActive = false;
            _myBoxCollider2D.enabled = false;
            _myAnimator.SetBool(ABILITY_ACTIVE_ANIMATION_BOOL_NAME, false);
            _myAnimator.SetBool(ABILITY_FINISH_ANIMATION_BOOL_NAME, true);
        }
        else if (IsActive)
        {
            _myAnimator.SetBool(ABILITY_START_ANIMATION_BOOL_NAME, false);
            _myAnimator.SetBool(ABILITY_ACTIVE_ANIMATION_BOOL_NAME, true);
        }
    }

    public override void ActiveTrigger()
    {
        base.ActiveTrigger();

        _myBoxCollider2D.enabled = true;
    }
}
