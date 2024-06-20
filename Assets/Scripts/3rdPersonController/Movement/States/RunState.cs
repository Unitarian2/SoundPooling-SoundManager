using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementManager)
    {
        movementManager.animator.SetBool("isRunning", true);
    }

    public override void UpdateState(MovementStateManager movementManager)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movementManager, movementManager.Walk);
        else if(movementManager.direction.magnitude < 0.1f) ExitState(movementManager, movementManager.Idle);

        if (movementManager.inputZ < 0) movementManager.currentMoveSpeed = movementManager.runBackSpeed;
        else movementManager.currentMoveSpeed = movementManager.runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementManager.PreviousState = this;
            ExitState(movementManager, movementManager.Jump);
        }
    }
    public override void ExitState(MovementStateManager movementManager, MovementBaseState newState)
    {
        movementManager.animator.SetBool("isRunning", false);
        movementManager.SwitchState(newState);
    }

}
