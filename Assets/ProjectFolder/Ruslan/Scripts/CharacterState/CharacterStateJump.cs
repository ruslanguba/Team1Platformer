using System.Collections;
using UnityEngine;

public class CharacterStateJump : CharacterStateBase
{
    public CharacterStateJump(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        //_ledgeHandler.OnLedgeGrabbed += GrabLedge;
        // Включаем анимацию и звук прыжка
        // _movement.PlayJumpAnimation();
        // _movement.PlayJumpSound();

        // Моментально переходим в состояние полета (без проверки скорости)
        _stateMachine.SetStateFall();
    }
}
