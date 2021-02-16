using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private float _inputHoldTime = 0.2f;

    public Vector2 RawMovementInput { get; private set; }
    public bool BombPlaceInput { get; private set; }
    private float _bombPlacedInputStartTime;

    public void OnMoveInput(CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
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
