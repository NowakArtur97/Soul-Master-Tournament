using UnityEngine;

public abstract class Soul : MonoBehaviour
{
    [SerializeField]
    protected D_SoulStats SoulStats;

    private Animator _myAnimator;

    protected bool IsExploding { get; private set; }
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

    protected virtual void Explode() { }

    protected virtual void SpawnExplosion(Vector2 explosionPosition) { }
}
