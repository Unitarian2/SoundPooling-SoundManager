using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    [HideInInspector] public int currentAmmo;

    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = clipSize;
    }


    public void Reload()
    {
        if(extraAmmo >= clipSize)
        {
            //Yedek mermi þarjörün mermi sayýsýndan fazlaysa veya eþitse -> ne kadar mermi lazýmsa o kadar mermi þarjöre ekliyoruz ve yedekten çýkarýyoruz.
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if(extraAmmo > 0)
        {
            if(extraAmmo + currentAmmo > clipSize)
            {
                //Yedek mermi þarjör miktarýndan fazla deðil ancak, þarjördeki mevcut mermi ile birlikte toplamý yeterli ise -> þarjörü fulluyoruz ve kalan mermiyi yedeðe aktarýyoruz.
                int leftoverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftoverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                //Yedek mermi var ama þarjörü fullemez ise -> Elimizdeki tüm yedek mermiyi þarjöre ekliyoruz.
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
