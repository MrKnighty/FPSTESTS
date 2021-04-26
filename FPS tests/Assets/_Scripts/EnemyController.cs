using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    bool isAgroo = false;
    bool hasShot;

    public GameObject muzzlePoint;
    public GameObject bullet;

    public float fireRate;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<NavMeshAgent>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) < 10) // if the player gets in a certan radius around the enemy, start targeting
        {
            isAgroo = true;
        }

        if(isAgroo)
        {
            agent.destination = player.transform.position;
            this.transform.LookAt(player.transform);
            if(Vector3.Distance(this.transform.position, player.transform.position) < 5) // wjen the AI gets within 5 units of the player, stop moving
            {
                agent.destination = this.transform.position;
            }

            if(!hasShot)
            {
                hasShot = true;
                GameObject spawnedBullet = Instantiate(bullet, muzzlePoint.transform); // create a variable of the spawned object so we can point it at the player
                spawnedBullet.transform.LookAt(player.transform.position);
                Invoke("ResetShoot", fireRate);
            }



        }
    }

    void ResetShoot()
    {
        hasShot = false;
    }


}
