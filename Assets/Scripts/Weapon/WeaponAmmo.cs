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
            //Yedek mermi �arj�r�n mermi say�s�ndan fazlaysa veya e�itse -> ne kadar mermi laz�msa o kadar mermi �arj�re ekliyoruz ve yedekten ��kar�yoruz.
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if(extraAmmo > 0)
        {
            if(extraAmmo + currentAmmo > clipSize)
            {
                //Yedek mermi �arj�r miktar�ndan fazla de�il ancak, �arj�rdeki mevcut mermi ile birlikte toplam� yeterli ise -> �arj�r� fulluyoruz ve kalan mermiyi yede�e aktar�yoruz.
                int leftoverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftoverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                //Yedek mermi var ama �arj�r� fullemez ise -> Elimizdeki t�m yedek mermiyi �arj�re ekliyoruz.
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
