using System.Collections;
using UnityEngine;

public class WinningMenu : MonoBehaviour
{
    [SerializeField]
    private float _startMovingAfter = 3;
    [SerializeField]
    private float _verticalSpeed = 20;
    [SerializeField]
    private float _verticalStartingOffset = 500;

    private RectTransform _myRectTransform;
    private Vector2 _verticalPosition;
    private bool _isMoving;

    private void Awake()
    {
        _isMoving = false;

        _myRectTransform = GetComponent<RectTransform>();
        _verticalPosition = _myRectTransform.anchoredPosition;
        _myRectTransform.anchoredPosition = new Vector2(_verticalPosition.x, _verticalPosition.y - _verticalStartingOffset);

        StartCoroutine(MovingCoroutine());
    }

    private IEnumerator MovingCoroutine()
    {
        yield return new WaitForSeconds(_startMovingAfter);

        _isMoving = true;
    }

    private void Update()
    {
        if (!_isMoving)
        {
            return;
        }

        Vector2 position = _myRectTransform.anchoredPosition;

        position.y += _verticalSpeed * Time.deltaTime;

        if (HasArrivedAtDestination(position))
        {
            _isMoving = false;
        }

        _myRectTransform.anchoredPosition = position;
    }

    private bool HasArrivedAtDestination(Vector2 position) => Vector3.Distance(position, _verticalPosition) < 3f;
}
