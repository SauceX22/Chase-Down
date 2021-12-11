using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public float hitForce = 3f;

    public GameObject ShotSmoke;
    public Camera fpsCam;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
      RaycastHit hit;

      if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
      {
           
        
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }


            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            if (target == null)
            {
               GameObject impactGO = Instantiate(ShotSmoke, hit.point, Quaternion.LookRotation(hit.normal));

                Destroy(impactGO, 2f);
            }
            
      }
        
         
    }

       
}
