using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actionManager)
    {
        //Reload Anim için eller serbest kalmalý.
        actionManager.rHandAim.weight = 0f;
        actionManager.lHandIK.weight = 0f;
        actionManager.animator.SetTrigger("isReloading");
    }

    public override void UpdateState(ActionStateManager actionManager)
    {
        
    }
}
