using UnityEngine;

public class PlayerInputData
{
    public Vector2 MoveDirection { get; private set; }
    public bool JumpPressed { get; private set; }

    public void SetMoveDirection(Vector2 direction) => MoveDirection = direction;
    public void SetJump(bool isPressed) => JumpPressed = isPressed;
}
