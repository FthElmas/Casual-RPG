using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        ActionScheduler action;
        
        private void Awake()
        {
            action = GetComponent<ActionScheduler>();
        }

       
       
        //The health decreases on specific damage input
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
    
        }

       
        public bool isDie()
        {
          return health == 0 ;
        }

        public bool StopOnDeath()
        {
            if(isDie())
            {
                action.CancelCurrentAction();
                return true;
            }
            return false;
        }
       
       


    }

}
