using UnityEngine;

public class WaterShield : SoulAbility
{
    //TODO: Ice and Water Soul Ability: Refactor 
    [SerializeField]
    private string _abilityTag;

    private int _shieldHealth;

    private void Start()
    {
        _shieldHealth = AbilityStats.abilityDexterity;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_abilityTag))
        {
            _shieldHealth--;

            if (_shieldHealth <= 0)
            {
                HasFinished = true;
            }
        }
    }

    public override void FinishTrigger() { }
}
