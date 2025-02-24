using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : EnemyBaseState
{
    public float attackTime = 2.0f;
    private float currentAttackTime = 0.0f;

    [SerializeField]
    private float angleChangeSpeed = 90.0f; // angle change a second

    private Rigidbody rb;

    public override void EnterState(StateMachine sm)
    {
        sm.animator.SetBool("isAttacking", true);
        currentAttackTime = attackTime;
        rb = sm.GetComponent<Rigidbody>();

        Debug.Log($"Distance: {Vector3.Distance(sm.player.transform.position, sm.AttackPoint.transform.position)}");
        if (Vector3.Distance(sm.player.transform.position, sm.AttackPoint.transform.position) <= 0.7f)
        {
            sm.player.SendMessage("applyDamage", 1);
        }
    }

    public override void update(StateMachine sm)
    {
        // look in the direction of the point
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(sm.player.position.x, rb.transform.position.y, sm.player.position.z) - rb.transform.position);

        rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, targetRotation, angleChangeSpeed * Time.deltaTime);

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
                    sm.enterNewState(sm.searchAreaState);
                }
            }
        }
    }
    public override void ExitState(StateMachine sm)
    {
    }
}
