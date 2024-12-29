using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Animator animator;

    public EnemyBaseState currentState;
    public Transform patrolPointsParent;

    public WaitAtPointState WaitAtPointState = new WaitAtPointState();
    public SeekNextPointState seekNextPointState = new SeekNextPointState();
    
    // Start is called before the first frame update
    void Start()
    {
        seekNextPointState.patrolPointsParent = patrolPointsParent;
        enterNewState(WaitAtPointState);
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
}
