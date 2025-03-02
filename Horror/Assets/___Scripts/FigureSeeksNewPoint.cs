using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FigureSeeksNewPoint : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform point;
    public Animator animator;
    private GameObject parent;

    private bool seekPoint = false;

    private void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            seekPoint = true;
            agent.SetDestination(point.position);
            animator.SetBool("IsWalking", true);
        }
    }

    private void Update()
    {
        if(seekPoint)
        {
            if(agent.remainingDistance <= 0)
            {
                Destroy(parent);
            }
        }
    }
}
