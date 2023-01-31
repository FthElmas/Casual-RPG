using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
   
   
    void Update(){

         if(Input.GetMouseButton(0))
         {
             MoveToCursor();
         }


    }
    private void MoveToCursor(){
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;

       bool hasHit =  Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            MoveTo(hit.point);
        }
    }

    protected void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }



}
