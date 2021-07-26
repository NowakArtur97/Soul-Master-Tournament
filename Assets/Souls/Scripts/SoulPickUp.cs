using UnityEngine;

public class SoulPickUp : MonoBehaviour
{
    [SerializeField]
    private D_SoulPickUp _pickUpData;

    private void Awake() => transform.position += (Vector3)_pickUpData.positionOffset;

    public D_SoulPickUp SoulData => _pickUpData;
}
