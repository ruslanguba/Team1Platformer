using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveInput;
    public event Action OnJumpInput;
    public event Action OnJumpCanceled;
    public event Action OnUseInput;
    public event Action OnHelpInput;
    private PlayerInput _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInput();
        _inputActions.Enable();
        
    }
    private void OnEnable()
    {
        _inputActions.Gameplay.Jump.performed += JumpPerformed;
        _inputActions.Gameplay.Jump.canceled += JumpCanceled;
        _inputActions.Gameplay.Movement.performed += MovementPerformed;
        _inputActions.Gameplay.Movement.canceled += MovementCanceled;
        _inputActions.Gameplay.Use.performed += UsePerformed;
        _inputActions.Gameplay.Help.performed += HelpPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Jump.performed -= JumpPerformed;
        _inputActions.Gameplay.Jump.canceled -= JumpCanceled;
        _inputActions.Gameplay.Movement.performed -= MovementPerformed;
        _inputActions.Gameplay.Movement.canceled -= MovementCanceled;
        _inputActions.Gameplay.Use.performed -= UsePerformed;
        _inputActions.Gameplay.Help.performed -= HelpPerformed;
        _inputActions.Disable();
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

    private void JumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpCanceled.Invoke();
    }

    private void HelpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHelpInput?.Invoke();
    }

    private void UsePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnUseInput?.Invoke();
    }
}
