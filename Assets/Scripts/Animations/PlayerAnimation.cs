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
    Health target;
    [SerializeField] private float weaponRange = 2f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    PlayerController control;
    private float timeSinceLastAttack = 0;
    Health healthComponent;
    private bool animCheck = false;
    ActionScheduler action;
    Fighter fightComponent;
    
    
    
    
    
    
    
    void Awake()
    {
        _anim = GetComponent<Animator>();
        control = GetComponent<PlayerController>();
        healthComponent = GetComponent<Health>();
        action = GetComponent<ActionScheduler>();
        fightComponent = GetComponent<Fighter>();
        
    }

    
    void Update()
    {
        
        UpdateAnimator();
        
        
        if(healthComponent.isDie())
        {   
            DeathAnimation();
        }
        
        if (!fightComponent.isInCombat())
        {
            ResetAttack();
        }
       if(fightComponent.isInCombat())
       {
        StopAttack();
       }
       
       

       
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
        
        target = combatTarget.GetComponent<Health>();
      
    }

    
    public void ResetAttack()
    {
        _anim.ResetTrigger("stopAttack");
    }
    public void StopAttack()
    {
        
       _anim.SetTrigger("stopAttack"); 
        
    }



    private void AttackAnimation()
    {
        if (target == null) return;
       
        if(target.isDie()) return;

        

        if(IsInRange() && timeSinceLastAttack > timeBetweenAttacks)
        {
            _anim.SetTrigger("attack");
            timeSinceLastAttack = 0 ;

        }
      
        
        
 
    }
    

    private void DeathAnimation()
    {
       

       if(animCheck) return;
        
        animCheck = true;

        _anim.SetTrigger("die");
        

    }

    
    public bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

   
   
  
    

  
}
}
