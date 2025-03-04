using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveInput;
    public event Action OnJumpInput;

    private PlayerInput _inputActions;
    private PlayerInputData _inputData;
    public PlayerInputData GetInputData() => _inputData;
    private bool _isMoving = false;
    private void Awake()
    {
        _inputData = new PlayerInputData();
        _inputActions = new PlayerInput();
        _inputActions.Enable();
        
    }
    private void OnEnable()
    {
        _inputActions.Gameplay.Jump.performed += JumpPerformed;
        _inputActions.Gameplay.Movement.performed += MovementPerformed;
        _inputActions.Gameplay.Movement.canceled += MovementCanceled;

    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Jump.performed -= JumpPerformed;
        _inputActions.Gameplay.Movement.performed -= MovementPerformed;
        _inputActions.Gameplay.Movement.canceled -= MovementCanceled;
    }

    private void MovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMoveInput?.Invoke(Vector2.zero);
        _isMoving = false;
    }

    private void MovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isMoving = true;
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpInput?.Invoke(); // Отправляем событие при прыжке
    }

    private void Update()
    {
        HandleMooveInput();
    }

    private void HandleMooveInput()
    {
        if (_isMoving)
        {
            Vector2 direction = _inputActions.Gameplay.Movement.ReadValue<Vector2>();
            OnMoveInput?.Invoke(direction);
        }
    }
}
