using UnityEngine;

public class IceWall : SoulAbility
{
    [SerializeField]
    private string _abilityTag;
    [SerializeField]
    private int _wallDexterity;

    private int _wallHealth;

    private void Start()
    {
        _wallHealth = _wallDexterity;
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
