using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
public class AIController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5f;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        Chase();
    }

    private void Chase()
    {
        if(DistanceToPlayer() < chaseDistance)
        {
            print(gameObject.name + "chase");
        }
    }

    private float DistanceToPlayer()
            {

                return Vector3.Distance(transform.position, player.transform.position);
            }
}
}
