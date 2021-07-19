using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private D_PlayerInputs _playerInputs;

    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public bool BombPlaceInput { get; private set; }

    private void Update()
    {
        OnMoveInput();
        OnBombPlaceInput();
    }

    public void OnMoveInput()
    {
        InputX = Input.GetAxisRaw(_playerInputs.horizontalAxis);
        InputY = Input.GetAxisRaw(_playerInputs.verticalAxis);
    }

    public void OnBombPlaceInput()
    {
        if (Input.GetKeyDown(_playerInputs.placeBomb))
        {
            BombPlaceInput = true;
        }
    }

    public void UseBombPlaceInput() => BombPlaceInput = false;
}
