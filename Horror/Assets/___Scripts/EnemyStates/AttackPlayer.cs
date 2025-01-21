using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : EnemyBaseState
{
    public float attackTime = 2.0f;
    private float currentAttackTime = 0.0f;

    public override void EnterState(StateMachine sm)
    {
        sm.animator.SetBool("isAttacking", true);
        currentAttackTime = attackTime;
    }

    public override void update(StateMachine sm)
    {
        if (currentAttackTime > 0.0f)
        {
            currentAttackTime -= Time.deltaTime;
            if (currentAttackTime <= 0.0f)
            {
                sm.animator.SetBool("isAttacking", false);
                if (sm.findLongRange())
                {
                    sm.enterNewState(sm.SeekPlayerState);
                }
                else
                {
                    sm.enterNewState(sm.waitAtPointState);
                }
            }
        }
    }
    public override void ExitState(StateMachine sm)
    {
    }
}
