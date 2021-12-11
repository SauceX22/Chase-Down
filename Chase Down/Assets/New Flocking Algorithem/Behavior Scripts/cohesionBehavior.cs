using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class cohesionBehavior : FilteredFlockBehavior
{
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
        return cohesionMove;
    }
}
