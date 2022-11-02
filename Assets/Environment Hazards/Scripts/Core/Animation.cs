using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animation : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void SetBoolVariable(string animationBoolName, bool value) => _animator.SetBool(animationBoolName, value);
}