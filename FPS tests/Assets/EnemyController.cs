using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Vector3 player;
    NavMeshAgent agent;
    bool isAgroo = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform.position;
        gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(Vector3.Distance(this.transform.position, player) < 10) // if the player gets in a certan radius around the enemy, start targeting
        {
            isAgroo = true;
        }

        if(isAgroo)
        {
            agent.destination = player;
        }
    }


}
