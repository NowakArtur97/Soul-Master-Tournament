using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private int _spritePerFrame = 6;
    [SerializeField]
    private bool _loop = true;
    [SerializeField]
    private bool _destroyOnEnd = false;

    private int _index;
    private Image _image;
    private int _frame;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _index = 0;
        _frame = 0;
    }

    private void Update()
    {
        if (!_loop && _index == _sprites.Length)
        {
            return;
        }

        _frame++;

        if (_frame < _spritePerFrame)
        {
            return;
        }

        _image.sprite = _sprites[_index];
        _frame = 0;
        _index++;

        if (_index >= _sprites.Length)
        {
            if (_loop) _index = 0;
            if (_destroyOnEnd) Destroy(gameObject);
        }
    }
}
