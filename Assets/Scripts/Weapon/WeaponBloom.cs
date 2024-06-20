using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations.Rigging;
using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] float defaultBloomAngle = 3;
    [SerializeField] float walkBloomMultiplier = 1.5f;
    [SerializeField] float crouchBloomMultiplier = 0.5f;
    [SerializeField] float runBloomMultiplier = 2f;
    [SerializeField] float adsBloomMultiplier = 0.5f;

    MovementStateManager movementStateManager;
    AimStateManager aimStateManager;

    float currentBloom;

    // Start is called before the first frame update
    void Start()
    {
        movementStateManager = GetComponentInParent<MovementStateManager>();
        aimStateManager = GetComponentInParent<AimStateManager>();

    }

    public Vector3 BloomAngle(Transform barrelPos)
    {
        //Ne kadar yüksek hýzda hareket ediyorsak bloom açýsý o kadar artacak
        if (movementStateManager.CurrentState == movementStateManager.Idle) currentBloom = defaultBloomAngle;
        else if(movementStateManager.CurrentState == movementStateManager.Walk) currentBloom = defaultBloomAngle * walkBloomMultiplier;
        else if(movementStateManager.CurrentState == movementStateManager.Run) currentBloom = defaultBloomAngle * runBloomMultiplier;
        else if(movementStateManager.CurrentState == movementStateManager.Crouch)
        {
            if(movementStateManager.direction.magnitude == 0) currentBloom = defaultBloomAngle * crouchBloomMultiplier;
            else currentBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;
        }

        if (aimStateManager.CurrentState == aimStateManager.Aim) currentBloom *= adsBloomMultiplier;

        float randX = Random.Range(-currentBloom, currentBloom);
        float randY = Random.Range(-currentBloom, currentBloom);
        float randZ = Random.Range(-currentBloom, currentBloom);

        Vector3 randomRot = new Vector3(randX, randY, randZ);

        //Bloom her seferinde farklý açýlarda parlamalý
        return barrelPos.localEulerAngles + randomRot;
    }
}
