using UnityEngine;

public class ObjectScaling : MonoBehaviour
{
    [SerializeField]
    private float _scale = 0.7f;

    private void Start() => transform.localScale = new Vector3(_scale, _scale, _scale);
}
