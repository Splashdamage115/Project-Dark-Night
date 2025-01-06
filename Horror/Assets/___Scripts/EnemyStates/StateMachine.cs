using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Animator animator;

    public GameObject materialHolder;
    private Material material;
    public float clipIncrease = 1.0f;
    public float clipAmt = 0.0f;

    public EnemyBaseState currentState;
    public Transform patrolPointsParent;

    public WaitAtPointState waitAtPointState = new WaitAtPointState();
    public SeekNextPointState seekNextPointState = new SeekNextPointState(); 
    public DeathState deathState = new DeathState();

    // Start is called before the first frame update
    void Start()
    {
        seekNextPointState.patrolPointsParent = patrolPointsParent;
        enterNewState(waitAtPointState);
        material = materialHolder.GetComponent<SkinnedMeshRenderer>().material;
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
        StartCoroutine(deleteObject());
    }
    IEnumerator deleteObject()
    {
        while (clipAmt < 1.0f)
        {
            clipAmt += clipIncrease * Time.deltaTime;
            material.SetFloat("_AlphaClip", clipAmt);
            yield return new WaitForNextFrameUnit();
        }
        Destroy(gameObject);
        yield return null;
    }
}
