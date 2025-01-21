using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SeekNextPointState : EnemyBaseState
{
    public Transform patrolPointsParent;
    private Transform currentSeekPoint;
    private int seekPointIndex = 0;

    public float speed = 1000.0f;

    Rigidbody rb;

    [SerializeField]
    private float angleChangeSpeed = 90.0f; // angle change a second
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

        rb = sm.GetComponent<Rigidbody>();
    }
    public override void update(StateMachine sm)
    {
        if (sm.findShortRange())
        {
            sm.enterNewState(sm.SeekPlayerState);
        }
        // look in the direction of the point
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentSeekPoint.position.x, rb.transform.position.y, currentSeekPoint.position.z) - rb.transform.position);
        rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, targetRotation, angleChangeSpeed * Time.deltaTime);

        // move forward along direction
        Vector3 moveDirection = rb.transform.forward;
        rb.velocity = moveDirection * speed * Time.deltaTime;


        // when the point is reached wait there
        if (Mathf.Sqrt(MathLibrary.squareDistancebetweenPoints(currentSeekPoint.position, rb.position)) <= 2.0f)
        {
            sm.enterNewState(sm.waitAtPointState);
        }
    }

    public override void ExitState(StateMachine sm) 
    {
        rb.velocity = Vector3.zero;
        sm.animator.SetBool("isWalking", false);
    }

}
