using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Bullet")
        {
            int chanceOfTeleportation = Random.Range(1, 4);

            if (chanceOfTeleportation <= 2)
            {
                //Teleporting
                Debug.Log("Teleporting!");
                float choice = Random.Range(1, 4);

                if (choice <= 2)
                {
                    //move Right!
                    transform.parent.GetComponent<EnemyController>().moveRight = true;
                }
                else if (choice >= 3)
                {
                    //move Left!
                    transform.parent.GetComponent<EnemyController>().moveLeft = true;
                }
            }
            else
            {
                //Not Teleporting
                Debug.Log("Nope!");
                return;
            }
            
        }
    }
}
