using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : EnemyBaseState
{
    public float speed = 1000.0f;

    private float lockY = 0.0f;

    Rigidbody rb;

    [SerializeField]
    private float angleChangeSpeed = 180.0f; // angle change a second
    public override void EnterState(StateMachine sm)
    {
        rb = sm.gameObject.GetComponent<Rigidbody>();
        sm.animator.SetBool("isWalking", true);
        lockY = rb.position.y;
    }

    public override void update(StateMachine sm)
    {
        // look in the direction of the point
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(sm.pos.x,rb.transform.position.y,sm.pos.z) - rb.transform.position);

        rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, targetRotation, angleChangeSpeed * Time.deltaTime);


        // move forward along direction
        Vector3 moveDirection = rb.transform.forward;
        rb.velocity = moveDirection * speed * Time.deltaTime;

        rb.MovePosition(new Vector3(rb.position.x, lockY, rb.position.z));
        // when the point is reached wait there
        if (Mathf.Sqrt(MathLibrary.squareDistancebetweenPoints(sm.player.position, rb.position)) <= 1.5f)
        {
            sm.enterNewState(sm.AttackPlayerState);
        }
    }
    public override void ExitState(StateMachine sm)
    {
        rb.velocity = Vector3.zero;
        sm.animator.SetBool("isWalking", false);
    }
}
