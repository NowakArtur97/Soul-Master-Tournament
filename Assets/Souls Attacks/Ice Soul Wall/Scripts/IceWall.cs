using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : SoulAbility
{
    [SerializeField]
    private int wallHealth;
    [SerializeField]
    private string _abilityTag;

    private void Update()
    {
        if (wallHealth <= 0)
        {
            HasFinished = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_abilityTag))
        {
            wallHealth--;
        }
    }
}