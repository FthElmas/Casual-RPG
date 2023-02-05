using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Control;
using RPG.Core;



namespace RPG.Anim
{
    public class PlayerAnimation : MonoBehaviour, IAttack
{   

    float speed;
    Animator _anim;
    Transform target;
    [SerializeField] private float weaponRange = 2f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    PlayerController control;
    private float timeSinceLastAttack = 0;

    
    
    
    
    
    void Awake()
    {
        _anim = GetComponent<Animator>();
        control = GetComponent<PlayerController>();

        
        
    }

    
    void Update()
    {
        UpdateAnimator();
        AttackAnimation();
        timeSinceLastAttack += Time.deltaTime;
        
    }
    

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        //Transforming global to local, so the animator can know easily
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        speed = localVelocity.z;
        _anim.SetFloat("forwardSpeed", speed);
    }

    public void Attack(CombatTarget combatTarget)
    {   
        
        target = combatTarget.transform;
      
    }

    private void AttackAnimation()
    {
        if (target == null) return;
       
        if(IsInRange() && timeSinceLastAttack > timeBetweenAttacks)
        {
            _anim.SetTrigger("attack");
            timeSinceLastAttack = 0 ;

        }
    }
    

    public bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

   

    

  
}
}
