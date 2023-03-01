using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        target = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        _anim = GetComponent<PlayerAnimation>();
    }
    private void Update()
    {   
        
        ChaseToAttack();
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
        }
           
        
    }

    private bool DistanceToPlayer()
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                return distanceToPlayer < chaseDistance;
            }

   
}
}
