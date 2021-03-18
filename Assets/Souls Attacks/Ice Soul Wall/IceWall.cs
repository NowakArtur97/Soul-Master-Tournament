using UnityEngine;

public class IceWall : SoulAbility
{
    [SerializeField]
    private string _abilityTag;

    private int _wallHealth;

    private void Start()
    {
        _wallHealth = AbilityStats.abilityDexterity;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_abilityTag))
        {
            _wallHealth--;

            if (_wallHealth <= 0)
            {
                HasFinished = true;
            }
        }
    }

    public override void FinishTrigger() { }
}
