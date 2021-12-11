using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int shotDamage = 10;
    public float destroyDelay = 3f;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        Target tar = hitInfo.GetComponent<Target>();

        if (hitInfo.tag == "Player")
        {
            return;
        }
        else if(hitInfo.tag == "bulletCol")
        {
            return;
        }
        else if(hitInfo.tag == "AiSphere")
        {
            return;
        }
        else if (tar != null)
        {
            //Debug.Log("we hit an enemy, sir!");
            tar.TakeDamage(shotDamage);
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log(hitInfo.name);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Destroy(gameObject, destroyDelay);
    }
}
