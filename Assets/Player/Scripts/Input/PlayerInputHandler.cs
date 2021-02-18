using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 _rawMovementInput;
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool BombPlaceInput { get; private set; }
    private float _bombPlacedInputStartTime;

    public void OnMoveInput(CallbackContext context)
    {
        _rawMovementInput = context.ReadValue<Vector2>();
        NormalizedInputX = Mathf.Abs(_rawMovementInput.x) > 0.5f ? (int)(_rawMovementInput * Vector2.right).normalized.x : 0;
        NormalizedInputY = Mathf.Abs(_rawMovementInput.y) > 0.5f ? (int)(_rawMovementInput * Vector2.up).normalized.y : 0;
    }

    public void OnBombPlaceInput(CallbackContext context)
    {
        if (context.started)
        {
            BombPlaceInput = true;
            _bombPlacedInputStartTime = Time.time;
        }
    }

    public void UseBombPlaceInput()
    {
        BombPlaceInput = false;
    }
}
