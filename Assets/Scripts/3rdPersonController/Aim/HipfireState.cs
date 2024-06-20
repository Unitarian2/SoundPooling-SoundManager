using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipfireState : AimBaseState
{
    public override void EnterState(AimStateManager aimManager)
    {
        aimManager.animator.SetBool("isAiming", false);
        aimManager.currentFov = aimManager.hipFov;
    }

    public override void UpdateState(AimStateManager aimManager)
    {
        if (Input.GetKey(KeyCode.Mouse1)) aimManager.SwitchState(aimManager.Aim);
    }

}
