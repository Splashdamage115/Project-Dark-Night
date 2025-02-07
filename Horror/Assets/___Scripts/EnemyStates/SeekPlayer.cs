using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : EnemyBaseState
{
    Rigidbody rb;

    public override void EnterState(StateMachine sm)
    {
        rb = sm.gameObject.GetComponent<Rigidbody>();
        sm.animator.SetBool("isRunning", true);
    }

    public override void update(StateMachine sm)
    {
        sm.agent.SetDestination(sm.player.position);

        if(sm.agent.remainingDistance < 5.0f)
        {
            if (sm.agent.remainingDistance < 1.0f)
            {
                if (sm.agent.hasPath)
                {
                    sm.enterNewState(sm.AttackPlayerState);
                }
            }
        }
        else 
        {
            sm.enterNewState(sm.searchAreaState);
        }
    }
    public override void ExitState(StateMachine sm)
    {
        sm.agent.ResetPath();
        rb.velocity = Vector3.zero;
        sm.animator.SetBool("isRunning", false);
        sm.animator.SetBool("isWalking", false);

    }
}
