using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Player Targeting")]
public class TargetPlayer : FlockBehavior
{
    Transform target;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, LayerMask mask)
    {
        target = PlayerManager.instance.Player.transform;

        Vector3 avoidanceMove = Vector3.zero;

        foreach (FlockAgent item in flock.agents)
        {
            avoidanceMove += (Vector3) target.position;
            //agent.Move(target.position);
        }
        return avoidanceMove;
    }
}
