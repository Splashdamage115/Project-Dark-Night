using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SeekNextPointState : EnemyBaseState
{
    public Transform patrolPointsParent;
    private Transform currentSeekPoint;
    private int seekPointIndex = 0;

    public float speed = 100.0f;

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
        // look in the direction of the point
        Quaternion targetRotation = Quaternion.LookRotation(rb.transform.position - currentSeekPoint.position);
        rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, targetRotation, angleChangeSpeed * Time.deltaTime);

        // move forward along direction
        Vector3 move = new Vector3(0.0f, 0.0f, -1.0f);
        rb.velocity = rb.transform.TransformDirection(move * speed * Time.deltaTime * 10);

        // when the point is reached wait there
        if (Mathf.Sqrt(MathLibrary.squareDistancebetweenPoints(currentSeekPoint.position, rb.position)) <= 1.0f)
        {
            sm.enterNewState(sm.waitAtPointState);
        }
    }

    public override void ExitState(StateMachine sm) {  }

}
