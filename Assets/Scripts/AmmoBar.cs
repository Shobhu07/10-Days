using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] private BulletWeapon Ammo;
    [SerializeField] private Image CurrentAmmoBar;


    void Update()
    {
        float fillAmount = (float)Ammo.currentAmmo / 30;
        CurrentAmmoBar.fillAmount = fillAmount;
        
    }
}
