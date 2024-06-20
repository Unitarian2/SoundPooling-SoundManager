using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementManager)
    {
        movementManager.animator.SetBool("isCrouching", true);
    }

    public override void UpdateState(MovementStateManager movementManager)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movementManager, movementManager.Run);
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(movementManager.direction.magnitude < 0.1f) ExitState(movementManager, movementManager.Idle);
            else ExitState(movementManager, movementManager.Walk);
        }

        if (movementManager.inputZ < 0) movementManager.currentMoveSpeed = movementManager.crouchBackSpeed;
        else movementManager.currentMoveSpeed = movementManager.crouchSpeed;
    }
    public override void ExitState(MovementStateManager movementManager, MovementBaseState newState)
    {
        movementManager.animator.SetBool("isCrouching", false);
        movementManager.SwitchState(newState);
    }
}
