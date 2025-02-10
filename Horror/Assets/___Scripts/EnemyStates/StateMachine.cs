using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class StateMachine : MonoBehaviour
{
    public float smallTriggerRadius = 1f;
    public float largeTriggerRadius = 2.0f;
    public bool waitAtPointPermanent = false;


    public NavMeshAgent agent;

    public Transform AttackPoint;

    public Transform player;

    public Animator animator;

    public GameObject materialHolder;

    public EnemyBaseState currentState;
    public Transform patrolPointsParent;

    public WaitAtPointState waitAtPointState = new WaitAtPointState();
    public SeekNextPointState seekNextPointState = new SeekNextPointState();
    public AttackPlayer AttackPlayerState = new AttackPlayer();
    public SeekPlayer SeekPlayerState = new SeekPlayer();
    public DeathState deathState = new DeathState();
    public SearchAreaState searchAreaState = new SearchAreaState();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        seekNextPointState.patrolPointsParent = patrolPointsParent;
        waitAtPointState.waitPermanent = waitAtPointPermanent;
        enterNewState(seekNextPointState);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Mathf.Sqrt(MathLibrary.DistancebetweenPoints(player.position, transform.position)) <= smallTriggerRadius)
        {
            return true;
        }

        return false;
    }

    public bool findLongRange()
    {
       
        if (Mathf.Sqrt(MathLibrary.DistancebetweenPoints(player.position, transform.position)) <= largeTriggerRadius)
        {
            return true;
        }

        return false;
    }
}
