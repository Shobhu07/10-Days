using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : MonoBehaviour
{
    public Transform firePoint;
    Vector2 direction;
    public GameObject bullet;
    public float BulletSpeed;
    public float fireRate;
    float ReadyForNextShot;
    public int maxAmmo = 10;
    public int currentAmmo; 
    

    void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)firePoint.position;
        FaceMouse();
        
        if (Input.GetButton("Fire1"))
        {
            if (Time.time > ReadyForNextShot)
            {
                ReadyForNextShot = Time.time + 1 / fireRate;
                Shoot();
                currentAmmo--;
            }
            
        }
    }
    

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            //currentAmmo--;
            GameObject BulletIns = Instantiate(bullet, firePoint.position, firePoint.rotation);
            BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);
            StartCoroutine(DestroyBulletAfterDistance(BulletIns));
        }
    }

    void FaceMouse()
    {
        firePoint.transform.right = direction;

    }
    IEnumerator DestroyBulletAfterDistance(GameObject bulletInstance)
    {
        float distanceTravelled = 0f;
        while (distanceTravelled < 1000f) 
        {
            distanceTravelled += BulletSpeed * Time.deltaTime;
            yield return null;
        }
        Destroy(bulletInstance);
    }

}
