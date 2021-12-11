using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    //==================================================================
    Transform target;
    NavMeshAgent agent;
    //==================================================================
    [HideInInspector]
    public bool moveRight = false;
    [HideInInspector]
    public bool moveLeft = false;
    //==================================================================
    public Transform Right;
    public Transform Left;
    public float dodgingSpeed = 6f;
    public float returnFalseTimer = 1;
    //==================================================================
    public float rotatingSpeed = 1f;
    public float sphereRadius = 2f;
    //==================================================================
    public bool spreadToRight = false;
    public bool spreadToLeft = false;
    public float spreadingAcceleration = 4;

    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        targetPlayer();

        if (spreadToRight)
        {
            //spread to the Right
            GetComponent<EnemyController>().MoveRightForSpread();
        }

        if (spreadToLeft)
        {
            //spred to the Left
            GetComponent<EnemyController>().MoveLeftForSpread();
        }

        if (moveRight)
        {
            //Teleport Right
            Debug.Log("We Moven Right!");
            transform.Translate(Vector3.right * dodgingSpeed * Time.deltaTime, Space.Self);
            StartCoroutine(MoveRight(returnFalseTimer));
        }

        if (moveLeft)
        {
            //Teleport Left
            Debug.Log("We Moven Left");
            transform.Translate(Vector3.left * dodgingSpeed * Time.deltaTime, Space.Self);
            StartCoroutine(MoveLeft(returnFalseTimer));
        }
    }

    public void MoveRightForSpread()
    {
        Debug.Log("We Spreading Right!");
        //agent.Move(Vector3.right);
        transform.Translate(Vector3.right, Space.Self);
    }

    public void MoveLeftForSpread()
    {
        Debug.Log("We Spreading Left!");
        //agent.Move(Vector3.left);
        transform.Translate(Vector3.left, Space.Self);
    }

    void targetPlayer()
    {
        //agent.SetDestination(target.position);

        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotatingSpeed * Time.deltaTime);
    }

    IEnumerator MoveRight(float timer)
    {
        yield return new WaitForSeconds(timer);
        moveRight = false;
    }
    IEnumerator MoveLeft(float timer)
    {
        yield return new WaitForSeconds(timer);
        moveLeft = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(Vector3.zero + new Vector3(0, 1, 0.75f), new Vector3(1.75f, 1.75f, 1.5f));

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireCube(Vector3.zero + new Vector3(0, 1, -2f), new Vector3(2, 2, 4));
    }
}
