using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Anim;


namespace RPG.Control
{
public class AIController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5f;
    GameObject player;
    Health target;
    PlayerAnimation _anim;
    Fighter fighter;
    Vector3 guardPosition;
    NavMeshAgent navMeshPosition;
    private float timeSinceLastSawPlayer = Mathf.Infinity;

    private void Awake()
    {
        navMeshPosition = GetComponent<NavMeshAgent>();
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
        

        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void ChaseToAttack()
    {
        if(DistanceToPlayer() && fighter.CanAttack(player))
        {
            fighter.Attack(player);
            _anim.Attack(player);
        }
        else
        {
            fighter.Cancel();
            BackToPlace();
        }
           
        
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

    private void BackToPlace()
    {
       
        navMeshPosition.destination = guardPosition;
        
    }
}
}
