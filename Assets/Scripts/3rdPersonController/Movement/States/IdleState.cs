using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementManager)
    {
        
    }

    public override void UpdateState(MovementStateManager movementManager)
    {
        if(movementManager.direction.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementManager.SwitchState(movementManager.Run);
            }
            else
            {
                movementManager.SwitchState(movementManager.Walk);
            }
        }

        if(Input.GetKeyDown(KeyCode.C)) movementManager.SwitchState(movementManager.Crouch);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementManager.PreviousState = this;
            movementManager.SwitchState(movementManager.Jump);
        }
    }
    public override void ExitState(MovementStateManager movementManager, MovementBaseState newState)
    {

    }
}
