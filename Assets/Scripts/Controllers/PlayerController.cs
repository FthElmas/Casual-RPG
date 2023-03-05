using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Combat;
using RPG.Anim;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
   
        
        Fighter fighter;
        PlayerAnimation _animation;
        Mover move;
        Health healthComponent;

        void Awake()
        {
            healthComponent = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            _animation = GetComponent<PlayerAnimation>();
            move = GetComponent<Mover>();
        }
        void Update()
            {
                if(healthComponent.StopOnDeath()) return;
                if(InteractWithCombat())  return; 
                if(InteractWithMovement()) return;
                    print("Out of map");
                
            }


       

        //Created an array of rays named hits, foreach ray that hits to the target so the player can attack and calls the attack animation
        public bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if(!(fighter.CanAttack(target.gameObject))) continue;

                
                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                    _animation.Attack(target.gameObject);
                    
                }
               
                return true;
            }
            return false;
            
        }

        //if the ray hits a place and gets an input, calls the function StartMoveAction
        private bool InteractWithMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;

            bool hasHit =  Physics.Raycast(GetMouseRay(), out hit);

                if (hasHit)
                {
                    if (Input.GetMouseButton(0))
                    {
                        move.StartMoveAction(hit.point);
                        
                        
                    }
                    return true;
                }
                return false;
            
        }
        
        //Takes a Vector and changes navmesh's position according to the vector
        

        //Creates a ray from camera to the screen
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        


    }

}