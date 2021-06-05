using UnityEngine;

public class SoulAbilityChildToParentTrigger : MonoBehaviour
{
    private SoulAbility _parentSoulAbility;

    private void Start() => _parentSoulAbility = transform.parent.GetComponent<SoulAbility>();

    private void OnTriggerEnter2D(Collider2D collision) => _parentSoulAbility.OnTriggerEnter2D(collision);
}
