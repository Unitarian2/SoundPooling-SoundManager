using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aimManager)
    {
        aimManager.animator.SetBool("isAiming", true);
        aimManager.currentFov = aimManager.adsFov;
    }

    public override void UpdateState(AimStateManager aimManager)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) aimManager.SwitchState(aimManager.Hip);
    }
}
