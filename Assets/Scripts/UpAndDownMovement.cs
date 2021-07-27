using UnityEngine;

public class UpAndDownMovement : MonoBehaviour
{
    [SerializeField]
    private float _speedDown = 0.5f;
    [SerializeField]
    private float _speedUp = 0.5f;
    [SerializeField]
    private float _units = 0.3f;

    private float _speed;
    private Vector3 _direction;
    private float _min;
    private float _max;

    void Start()
    {
        _max = transform.position.y;
        _min = transform.position.y - _units;

        _direction = Vector3.down;
    }

    void Update()
    {
        if (_direction == Vector3.down)
        {
            _speed = _speedDown;
        }
        else if (_direction == Vector3.up)
        {
            _speed = _speedUp;
        }

        transform.Translate(_direction * _speed * Time.deltaTime);

        if (transform.position.y >= _max)
        {
            _direction = Vector3.down;
        }

        if (transform.position.y <= _min)
        {
            _direction = Vector3.up;
        }
    }
}
