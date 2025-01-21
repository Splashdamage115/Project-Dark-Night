using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAtPointState : EnemyBaseState
{
    public float waitAtPointTime = 2.0f;
    private float currentWaitAtPointTime = 0.0f;

    public override void EnterState(StateMachine sm)
    {
        sm.animator.SetBool("isWalking", false);
        currentWaitAtPointTime = waitAtPointTime;
    }
    public override void update(StateMachine sm)
    {
        if (sm.findLongRange())
        {
            sm.enterNewState(sm.SeekPlayerState);
        }
        if (currentWaitAtPointTime > 0.0f)
        {
            currentWaitAtPointTime -= Time.deltaTime;
            if (currentWaitAtPointTime <= 0.0f)
            {
                sm.enterNewState(sm.seekNextPointState);
            }
        }
    }

    public override void ExitState(StateMachine sm) { }
}
