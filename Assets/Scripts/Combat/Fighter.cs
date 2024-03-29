using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
   public class Fighter : MonoBehaviour, IAction, IAttack
   {
    [SerializeField] private float weaponRange = 2f;
    Health target;
    Mover move;
    ActionScheduler action;
    Health targetHealth;

    [SerializeField]float weaponDamage = 5f ;
    
   
    
    void Awake()
    {
        move = GetComponent<Mover>();
        action = GetComponent<ActionScheduler>();
        targetHealth = GetComponent<Health>();
        
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
                move.MoveTo(target.transform.position, 1f);
            }
            else
            {
                move.Cancel();
                transform.LookAt(target.transform);
                
            }
        }

        //checks the distance between enemytarget and player
        public bool GetIsInRange()
            {
                return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
            }

    
        // combat mode for the player when player encounters an enemy
        public void Attack(GameObject combatTarget)
            {
                action.StartAction(this);
                target = combatTarget.GetComponent<Health>();
                
            }

        // Hit() is called on the exact impact moment to the target by the animator
        public void Hit()
        {   
            if(target == null) return;
            target.TakeDamage(weaponDamage);
        }
        public bool isInCombat()
        {   
            if(target != null) return false;
            return target == null;
        }
        public void Cancel()
        {
            target = null;
            move.Cancel();
        }


        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;

            targetHealth = combatTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.isDie();


        } 

    }

    

}
