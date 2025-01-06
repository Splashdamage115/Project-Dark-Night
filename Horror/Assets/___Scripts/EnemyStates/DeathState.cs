using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyBaseState
{
    public float currentWaitTime = 0.8f;
    public override void EnterState(StateMachine sm)
    {
        sm.animator.SetBool("Dying", true);
    }
    public override void update(StateMachine sm)
    {
        if (currentWaitTime > 0.0f)
        {
            currentWaitTime -= Time.deltaTime;
            if (currentWaitTime <= 0.0f)
            {
                sm.Destroy();
            }
        }
    }
    public override void ExitState(StateMachine sm)
    {
        
    }
}
