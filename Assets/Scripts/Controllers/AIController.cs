using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Anim;
using System;
using RPG.Movement;

namespace RPG.Control
{
public class AIController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField]private float wayPointTolerance = 1f;
    [SerializeField]private float suspicionTime = 5f;
    [SerializeField]PatrolPathControl patrolPath;
    [SerializeField]private float waypointDwellTime = 3f;
    


    Vector3 guardPosition;
    private float timeSinceLastSawPlayer = Mathf.Infinity;
    private float timeSinceLastWaypoint = Mathf.Infinity;
    protected int currentWaypointIndex = 0;
    


    GameObject player;
    Health target;
    PlayerAnimation _anim;
    Fighter fighter;
    Mover mover;
    ActionScheduler aiAction;

    private void Awake()
    {
        aiAction = GetComponent<ActionScheduler>();
        mover = GetComponent<Mover>();
        player = GameObject.FindWithTag("Player");
        target = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        _anim = GetComponent<PlayerAnimation>();
        guardPosition = transform.position;
        
    }
    private void Update()
    {   
        if(target.StopOnDeath()) return;
        
        ChaseToAttack();
        
        UpdateTimers();
        
        
    }

    private void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceLastWaypoint += Time.deltaTime;
    }

    private void ChaseToAttack()
    {
        if(DistanceToPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
        {
            SuspicionBehaviour();
        }
        else
        {
            
            PatrolBehaviour();
        }
           
        
    }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
            _anim.Attack(player);
        }

        private bool DistanceToPlayer()
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                return distanceToPlayer < chaseDistance;
            }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;

        if (patrolPath != null)
        {
            if(AtWayPoint())
            {
                timeSinceLastWaypoint = 0;
                CycleWayPoint();
            }
            
            nextPosition = GetCurrentWaypoint();
        }
        if(timeSinceLastWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition);

        }
        
    }
        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < wayPointTolerance;
        }
        private void CycleWayPoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        

        

        private void SuspicionBehaviour()
    {
        aiAction.CancelCurrentAction();
    }
}
}
