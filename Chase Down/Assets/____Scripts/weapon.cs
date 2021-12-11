using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoiont;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            FindObjectOfType<AudioManager>().Play("Shooting");
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoiont.position, firePoiont.rotation);
    }

}
