using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementManager)
    {
        movementManager.animator.SetBool("isWalking", true);
    }

    public override void UpdateState(MovementStateManager movementManager)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movementManager, movementManager.Run);
        else if(Input.GetKeyDown(KeyCode.C)) ExitState(movementManager, movementManager.Crouch);
        else if(movementManager.direction.magnitude < 0.1f) ExitState(movementManager, movementManager.Idle);

        if (movementManager.inputZ < 0) movementManager.currentMoveSpeed = movementManager.walkBackSpeed;
        else movementManager.currentMoveSpeed = movementManager.walkSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementManager.PreviousState = this;
            ExitState(movementManager, movementManager.Jump);   
        }
    }

    public override void ExitState(MovementStateManager movementManager, MovementBaseState newState)
    {

        movementManager.animator.SetBool("isWalking", false);
        movementManager.SwitchState(newState);
    }


}
