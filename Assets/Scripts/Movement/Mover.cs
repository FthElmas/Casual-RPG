using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
public class Mover : MonoBehaviour, IAction
{
    ActionScheduler action;
    NavMeshAgent navMeshAgent;
    Health health;

    private void Awake()
    {   
        health = GetComponent<Health>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        action = GetComponent<ActionScheduler>();
    }

    private void Update()
    {
        DisableNavMeshOnDeath();
    }
    public void StartMoveAction(Vector3 destination)
            {
                action.StartAction(this);
                MoveTo(destination);
            }
    
    public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

    public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

    public void DisableNavMeshOnDeath()
    {
        navMeshAgent.enabled = !health.StopOnDeath();
    }
}

}
