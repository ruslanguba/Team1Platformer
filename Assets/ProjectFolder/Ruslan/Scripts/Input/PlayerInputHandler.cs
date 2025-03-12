using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveInput;
    public event Action OnJumpInput;
    public event Action OnUseInput;
    private PlayerInput _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInput();
        _inputActions.Enable();
        
    }
    private void OnEnable()
    {
        _inputActions.Gameplay.Jump.performed += JumpPerformed;
        _inputActions.Gameplay.Movement.performed += MovementPerformed;
        _inputActions.Gameplay.Movement.canceled += MovementCanceled;
        _inputActions.Gameplay.Use.performed += UsePerformed;

    }

    private void UsePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnUseInput?.Invoke();
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Jump.performed -= JumpPerformed;
        _inputActions.Gameplay.Movement.performed -= MovementPerformed;
        _inputActions.Gameplay.Movement.canceled -= MovementCanceled;
        _inputActions.Gameplay.Use.performed -= UsePerformed;
    }

    private void MovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMoveInput?.Invoke(Vector2.zero);
    }

    private void MovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 direction = _inputActions.Gameplay.Movement.ReadValue<Vector2>();
        OnMoveInput?.Invoke(direction);
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpInput?.Invoke();
    }
}
