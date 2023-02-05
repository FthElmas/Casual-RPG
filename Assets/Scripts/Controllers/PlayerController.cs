using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Combat;
using RPG.Anim;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour, IAction
    {
   
        public NavMeshAgent navMeshAgent;
        Fighter fighter;
        PlayerAnimation _animation;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            fighter = GetComponent<Fighter>();
            _animation = GetComponent<PlayerAnimation>();
        }
        void Update()
            {
                if(InteractWithCombat())  return; 
                if(InteractWithMovement()) return;
                    print("Out of map");
                
            }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        //Created an array of rays named hits, foreach ray that hits to the target so the player can attack and calls the attack animation
        public bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;
                
                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
                        _animation.Attack(target);
                    
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
                        StartMoveAction(hit.point);
                        
                        
                    }
                    return true;
                }
                return false;
            
        }
        
        //Takes a Vector and changes navmesh's position according to the vector
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        //Creates a ray from camera to the screen
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        


    }

}