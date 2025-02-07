using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAreaState : EnemyBaseState
{
    public float waitAtPointTime = 2.0f;
    private float currentWaitAtPointTime = 0.0f;

    public override void EnterState(StateMachine sm)
    {
        currentWaitAtPointTime = waitAtPointTime;
    }
    public override void update(StateMachine sm)
    {
        if (currentWaitAtPointTime > 0.0f)
        {
            currentWaitAtPointTime -= Time.deltaTime;
            if (currentWaitAtPointTime <= 0.0f)
            {
                if (sm.findLongRange())
                {
                    sm.enterNewState(sm.SeekPlayerState);
                }
                else
                {
                    sm.enterNewState(sm.seekNextPointState);
                }
            }
        }
    }
    public override void ExitState(StateMachine sm)
    {
        sm.animator.SetBool("isWalking", false);
    }
}
