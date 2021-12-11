using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    public Transform originL;
    public Transform originR;

    private Vector3 originC;
    private Vector3 direction;

    public Vector3 OriginC { get { originC = transform.localPosition + new Vector3(0, 1.5f, 0);
            return originC;
        } }

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    private int lockRot = 0;

    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(lockRot, transform.rotation.eulerAngles.y, lockRot);
    }

    private void OnDrawGizmosSelected()
    {
        AgentFlock.DrawGizmos();
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
