using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;
using RPG.Core;

namespace RPG.Combat
{
   public class Fighter : MonoBehaviour, IAction, IAttack
   {
    [SerializeField] private float weaponRange = 2f;
    Transform target;
    PlayerController control;
    ActionScheduler action;
    Health targetHealth;
    [SerializeField]float weaponDamage = 5f ;
    
   
    
    void Awake()
    {
        control = GetComponent<PlayerController>();
        action = GetComponent<ActionScheduler>();
        
        
    }
    private void Update()
        {
            MoveAccording();
            
        }

        public void MoveAccording()
        {
            if (target == null) return;

            if (!GetIsInRange())
            {
                control.MoveTo(target.position);
            }
            else
            {
                control.Cancel();
                
                
            }
        }

        //checks the distance between enemytarget and player
        public bool GetIsInRange()
            {
                return Vector3.Distance(transform.position, target.position) < weaponRange;
            }

    
        // combat mode for the player when player encounters an enemy
        public void Attack(CombatTarget combatTarget)
            {
                action.StartAction(this);
                target = combatTarget.transform;
                
            }

        // Hit() is called on the exact impact moment to the target by the animator
        public void Hit()
        {   
            targetHealth = target.GetComponent<Health>();
            targetHealth.TakeDamage(weaponDamage);
        }
        public void Cancel()
        {   
                
            target = null;
        }
    }

    

}
