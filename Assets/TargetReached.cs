using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class TargetReached : AIPath
{
    private Transform nextTarget;
    private bool arrived = false;
    public AIDestinationSetter moveTo;

    public bool SetNextTarget(Transform newTarget)
        {
            nextTarget = newTarget;
            if(arrived)
            {
                arrived = false;
                return true;
            }
            arrived = false;
            return false;
        }

    public override void OnTargetReached()
    {
        gameObject.GetComponent<AIDestinationSetter>().target = nextTarget;
        arrived = true;
    }


    private void SetMoveTo(Transform target)
    {
        if (moveTo.target != target) moveTo.target = target;
    }
}


