using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    [SerializeField]
    protected D_SoulStats SoulStats;
    [SerializeField]
    protected GameObject Explosion;

    private Animator _myAnimator;

    protected bool IsExploding { get; private set; }
    protected bool HasExploded;
    protected bool ShouldStartSpawningExplosions;
    protected Vector2[] ExplosionDirections { get; private set; }
    private float _timeToExplode;

    protected virtual void Awake()
    {
        _myAnimator = GetComponentInChildren<Animator>();

        ExplosionDirections = SoulStats.directions;
        _timeToExplode = SoulStats.timeToExplode;
    }

    protected virtual void Update()
    {
        if (Time.time > _timeToExplode)
        {
            IsExploding = true;
        }
    }

    protected virtual void Explode()
    {
        _myAnimator.SetBool("explode", true);
    }

    protected virtual void SpawnExplosion(Vector2 explosionPosition) { }

    public virtual void ExplodedTrigger()
    {
        HasExploded = true;
    }

    public virtual void StartSpawningExplosionsTrigger()
    {
        ShouldStartSpawningExplosions = true;
    }
}
