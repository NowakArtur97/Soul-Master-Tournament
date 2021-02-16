using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    [SerializeField]
    protected D_SoulStats SoulStats;
    [SerializeField]
    protected GameObject Explosion;

    private GameObject _aliveGameObject;
    private SoulAnimationToComponent _soulAnimationToComponent;
    protected Animator MyAnimator { get; private set; }

    protected bool IsExploding { get; private set; }
    protected bool HasExploded;
    protected bool ShouldStartSpawningExplosions;
    protected Vector2[] ExplosionDirections { get; private set; }
    private float _timeToExplode;
    private float _startTime;

    protected virtual void Awake()
    {
        _aliveGameObject = transform.Find("Alive").gameObject;
        MyAnimator = _aliveGameObject.GetComponent<Animator>();
        _soulAnimationToComponent = _aliveGameObject.GetComponent<SoulAnimationToComponent>();

        _soulAnimationToComponent.Soul = this;

        ExplosionDirections = SoulStats.directions;
        _timeToExplode = SoulStats.timeToExplode;

        _startTime = Time.time;
    }

    protected virtual void Update()
    {
        if (Time.time > _startTime + _timeToExplode)
        {
            IsExploding = true;
        }
    }

    protected virtual void Explode()
    {
        MyAnimator.SetBool("explode", true);
    }

    protected virtual void SpawnExplosion(Vector2 explosionDirection) { }

    public virtual void ExplodedTrigger()
    {
        HasExploded = true;
    }

    public virtual void StartSpawningExplosionsTrigger()
    {
        ShouldStartSpawningExplosions = true;
    }
}
