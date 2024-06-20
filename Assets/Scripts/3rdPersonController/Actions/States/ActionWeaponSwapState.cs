using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWeaponSwapState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionManager)
    {
        actionManager.animator.SetTrigger("isSwapWeapon");
        actionManager.lHandIK.weight = 0f;
        actionManager.rHandAim.weight = 0f;

    }

    public override void UpdateState(ActionStateManager actionManager)
    {
        
    }
}
