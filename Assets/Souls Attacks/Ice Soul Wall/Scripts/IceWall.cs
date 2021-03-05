using UnityEngine;

public class IceWall : SoulAbility
{
    [SerializeField]
    private string _abilityTag;

    private int wallHealth;

    private void Start()
    {
        wallHealth = AbilityStats.abilityDexterity;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_abilityTag))
        {
            wallHealth--;

            if (wallHealth <= 0)
            {
                HasFinished = true;
            }
        }
    }

    public override void FinishTrigger() { }
}
