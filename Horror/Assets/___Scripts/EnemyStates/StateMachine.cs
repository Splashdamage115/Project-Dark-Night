using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public float smallTriggerRadius = 2f;
    public float largeTriggerRadius = 3.0f;

    public Transform player;

    public Vector3 pos;

    public Animator animator;

    public GameObject materialHolder;

    public EnemyBaseState currentState;
    public Transform patrolPointsParent;

    public WaitAtPointState waitAtPointState = new WaitAtPointState();
    public SeekNextPointState seekNextPointState = new SeekNextPointState();
    public AttackPlayer AttackPlayerState = new AttackPlayer();
    public SeekPlayer SeekPlayerState = new SeekPlayer();
    public DeathState deathState = new DeathState();

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;
        seekNextPointState.patrolPointsParent = patrolPointsParent;
        enterNewState(waitAtPointState);
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.position;
        currentState.update(this);
    }

    public void enterNewState(EnemyBaseState newState)
    {
        currentState?.ExitState(this);

        if(newState != null || currentState != newState)
            currentState = newState;

        currentState.EnterState(this);
    }

    void Death()
    {
        enterNewState(deathState);
    }

    public void Destroy()
    {
        FadeAndDestroy t = gameObject.AddComponent<FadeAndDestroy>();
        t.materialHolder = materialHolder;
    }

    public bool findShortRange()
    {
        if (Mathf.Sqrt(MathLibrary.squareDistancebetweenPoints(player.position, transform.position)) <= smallTriggerRadius)
        {
            return true;
        }

        return false;
    }

    public bool findLongRange()
    {
       
        if (Mathf.Sqrt(MathLibrary.squareDistancebetweenPoints(player.position, transform.position)) <= largeTriggerRadius)
        {
            return true;
        }

        return false;
    }
}
