using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 newPosition = PlayerManager.instance.Player.transform.position; ;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
