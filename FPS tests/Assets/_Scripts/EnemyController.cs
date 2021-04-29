using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    public bool isAgroo = false;
    bool hasShot;
    public bool playerDead;

    public GameObject muzzlePoint;
    public GameObject bullet;
    
    public float agrooDistance;
    public float agrooSeeDistance;
    public GameObject head;

    public float fireRate;

    public LayerMask playerLM;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<NavMeshAgent>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
        head.transform.LookAt(player.transform);
        RaycastHit hit;
        if(Physics.Raycast(head.transform.position, head.transform.forward, out hit))
        {
            if(hit.transform.tag != "Player")
            {
                isAgroo = false;
            }
        }

        if(isAgroo && !playerDead)
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
        else
        {
          if(Vector3.Distance(this.transform.position, player.transform.position) <= agrooDistance)
          {
              isAgroo = true;
          }

            

          if(Physics.Raycast(head.transform.position, head.transform.forward, out hit, agrooSeeDistance))
          {
              if(hit.transform.tag == "Player")
              {
                  isAgroo = true;
              }
          }
        
            agent.destination = this.transform.position;
        }
    }

    void ResetShoot()
    {
        hasShot = false;
    }

    void CheckToAgroo()
    {

    }


}
