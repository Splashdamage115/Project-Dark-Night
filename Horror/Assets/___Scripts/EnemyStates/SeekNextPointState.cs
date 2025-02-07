using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SeekNextPointState : EnemyBaseState
{
    public Transform patrolPointsParent;
    private Transform currentSeekPoint;
    private int seekPointIndex = 0;

    Rigidbody rb;

    public override void EnterState(StateMachine sm)
    {
        sm.animator.SetBool("isWalking", true);

        // choose a random child to move towards, this is for more random movement
        if (patrolPointsParent.childCount >= 2)
        {
            int randomPoint = Random.Range(0, patrolPointsParent.childCount);
            while (randomPoint == seekPointIndex) randomPoint = Random.Range(0, patrolPointsParent.childCount);
            seekPointIndex = randomPoint;
        }
        else
        {
            seekPointIndex = 0;
        }
        currentSeekPoint = patrolPointsParent.GetChild(seekPointIndex);

        sm.agent.SetDestination(currentSeekPoint.position);

    }
    public override void update(StateMachine sm)
    {
        if (sm.findShortRange())
        {
            sm.enterNewState(sm.SeekPlayerState);
        }

        if (sm.agent.remainingDistance < 0.3f)
        {
            sm.enterNewState(sm.waitAtPointState);
        }
    }

    public override void ExitState(StateMachine sm) 
    {
        sm.animator.SetBool("isWalking", false);
    }

}
