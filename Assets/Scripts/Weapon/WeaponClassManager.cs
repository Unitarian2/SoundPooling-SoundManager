using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    public Transform recoilFollowPos;
    ActionStateManager actionStateManager;

    public WeaponManager[] weapons;
    int currentWeaponIndex;

    private void Awake()
    {
        currentWeaponIndex = 0;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == 0) weapons[i].gameObject.SetActive(true);
            else weapons[i].gameObject.SetActive(false);
        }
    }

    public void SetCurrentWeapon(WeaponManager weaponManager)
    {
        if(actionStateManager == null) actionStateManager = GetComponent<ActionStateManager>();
        leftHandIK.data.target = weaponManager.leftHandTarget;
        leftHandIK.data.hint = weaponManager.leftHandHint;

        actionStateManager.SetWeapon(weaponManager);
    }

    public void ChangeWeapon(float swapDirection)
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        if(swapDirection < 0)
        {
            if (currentWeaponIndex == 0) currentWeaponIndex = weapons.Length - 1;
            else currentWeaponIndex--;
        }
        else
        {
            if (currentWeaponIndex == weapons.Length - 1) currentWeaponIndex = 0;
            else currentWeaponIndex++;
        }

        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void WeaponPutAway()
    {
        ChangeWeapon(actionStateManager.Default.scrollDirection);
    }

    public void WeaponPulledOut()
    {
        actionStateManager.SwitchState(actionStateManager.Default);
    }
}
