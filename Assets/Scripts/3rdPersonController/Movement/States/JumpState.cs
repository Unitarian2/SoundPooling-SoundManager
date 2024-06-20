using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementManager)
    {
        if (movementManager.PreviousState == movementManager.Idle) movementManager.animator.SetTrigger("isIdleJumping");
        else if (movementManager.PreviousState == movementManager.Walk || movementManager.PreviousState == movementManager.Run) movementManager.animator.SetTrigger("isRunJumping");
    }

    public override void ExitState(MovementStateManager movementManager, MovementBaseState newState)
    {
        
    }

    public override void UpdateState(MovementStateManager movementManager)
    {
        if(movementManager.jumped && movementManager.IsGrounded())
        {
            Debug.Log("JumpCompleted");
            movementManager.jumped = false;//Z�plama s�reci tamamlanacak
            //Direction yerine input'lar� check ediyoruz ��nk� z�plaman�n ortas�nda ko�ma tu�una bas�labilir bu durumda karakter hareket etmese bile i�in sonunda ko�ma animasyonuna ge�mek gerekir.
            if (movementManager.inputX < 0.01f && movementManager.inputZ < 0.01f) movementManager.SwitchState(movementManager.Idle);
            else if(Input.GetKey(KeyCode.LeftShift)) movementManager.SwitchState(movementManager.Run);
            else movementManager.SwitchState(movementManager.Walk);
        }
    }
}
