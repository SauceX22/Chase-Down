using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Obstacale Avoidance")]
public class ObstacaleAvoidanceBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, LayerMask mask)
    {
        //if no neighbors, return no adjustments.
        if (context.Count == 0)
            return Vector3.zero;

        //add all points together, and average
        Vector3 originC = agent.OriginC;
        Transform originL = agent.originL.transform;
        Transform originR = agent.originR.transform;

        float maxDistance = flock.rayMaxDistance;
        Vector3 direction = agent.transform.forward;

        RaycastHit hit;

        Vector3 avoidanceMove = Vector3.zero;
        Vector3 hitPoint = Vector3.zero;

        if (originL == null)
        {
            Debug.LogWarning("L is Null!");
        }
        if (originR == null)
        {
            Debug.LogWarning("R is Null!");
        }
        if (originC == null)
        {
            Debug.LogWarning("C is Null!");
        }

        bool first = Physics.Raycast(originC, direction, out hit, maxDistance, mask);
        //bool second = Physics.Raycast(originR.position, direction, out hit, maxDistance, mask);
        //bool third = Physics.Raycast(originL.position, direction, out hit, maxDistance, mask);

        if (first)
        {
            avoidanceMove += (Vector3) (agent.OriginC - hit.point);
        }
            return avoidanceMove;
    }
}
