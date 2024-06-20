using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Settings")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    
    public float weaponBulletDamage = 20f;
    public float weaponBulletKickBackForce = 100f;

    [Header("Sound Settings")]
    [SerializeField] private OneShotSFX weaponGunShotSound;
    [SerializeField] AudioClip gunShotSound;
    [HideInInspector] public AudioSource audioSource;

    [Header("Muzzle Light Settings")]
    [SerializeField] float lightReturnSpeed;

    //References
    [HideInInspector] public WeaponAmmo ammo;
    AimStateManager aimManager;
    ActionStateManager actionManager;
    WeaponRecoil recoil;
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    WeaponBloom bloom;
    WeaponClassManager weaponClassManager;
    float lightIntensity;

    [Header("Weapon Switching Settings")]
    public Transform leftHandTarget, leftHandHint;


    // Start is called before the first frame update
    void Start()
    {
        aimManager = GetComponentInParent<AimStateManager>();
        actionManager = GetComponentInParent<ActionStateManager>();

        bloom = GetComponent<WeaponBloom>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;  
    }

    private void OnEnable()
    {
        if(weaponClassManager == null)
        {
            weaponClassManager = GetComponentInParent<WeaponClassManager>();
            ammo = GetComponent<WeaponAmmo>();
            audioSource = GetComponent<AudioSource>();
            recoil = GetComponent<WeaponRecoil>();
            recoil.recoilFollowPos = weaponClassManager.recoilFollowPos;
        }

        weaponClassManager.SetCurrentWeapon(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanFire()) Fire();
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool CanFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;//Þarjör boþ.
        if (actionManager.currentState == actionManager.Reload) return false;//Reload'dayýz.
        if (actionManager.currentState == actionManager.WeaponSwap) return false;//Silah swaplýyoruz.
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;//Semi Automatic
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;//Fully Automatic
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        ammo.currentAmmo--;

        barrelPos.LookAt(aimManager.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);

        audioSource.PlayOneShot(gunShotSound);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet,barrelPos.position,barrelPos.rotation);

            currentBullet.GetComponent<Bullet>().InitializeBullet(weaponBulletDamage, barrelPos.transform.forward, weaponBulletKickBackForce);

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}
