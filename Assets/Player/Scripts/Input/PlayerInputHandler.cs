using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private float _inputHoldTime = 0.2f;

    public Vector2 RawMovementInput { get; private set; }
    public bool BombPlacedInput { get; private set; }
    private float _bombPlacedInputStartTime;

    private void Update()
    {
        CheckBombPlacedInput();
    }

    public void OnMoveInput(CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
    }

    public void OnBombPlacedInput(CallbackContext context)
    {
        if (context.started)
        {
            BombPlacedInput = true;
            _bombPlacedInputStartTime = Time.time;
        }
    }

    private void CheckBombPlacedInput()
    {
        if (Time.time >= _bombPlacedInputStartTime + _inputHoldTime)
        {
            BombPlacedInput = false;
        }
    }
}
