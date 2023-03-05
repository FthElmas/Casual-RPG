using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;


    void LateUpdate()
    {
        transform.position = target.position;
    }

    private void RotateCamera()
    {
        if(Input.GetMouseButtonDown(1))
        {
            
        }
    }
}
}
