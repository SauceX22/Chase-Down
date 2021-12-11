using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredcohesionBehavior : FilteredFlockBehavior
{
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, LayerMask mask)
    {
        //if no neighbors, return no adjustments.
        if (context.Count == 0)
            return Vector3.zero;

        //add all points together, and average
        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filrteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filrteredContext)
        {
            cohesionMove += (Vector3)item.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= (Vector3)agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
