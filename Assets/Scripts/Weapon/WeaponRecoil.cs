using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public Transform recoilFollowPos;
    [SerializeField] float  kickBackAmount;
    [SerializeField] float kickBackSpeed, returnSpeed;

    float currentRecoilPos, finalRecoilPos;

    // Update is called once per frame
    void Update()
    {
        currentRecoilPos = Mathf.Lerp(currentRecoilPos, 0, returnSpeed * Time.deltaTime);
        finalRecoilPos = Mathf.Lerp(finalRecoilPos, currentRecoilPos, kickBackSpeed * Time.deltaTime);
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPos);
    }

    public void TriggerRecoil() => currentRecoilPos += kickBackAmount;
}
