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
    public bool useInnacuracy;
    public float bulletSpred;

    public GameObject muzzlePoint;
    public GameObject bullet;
    
    public float agrooDistance;
    public float agrooSeeDistance;
    public GameObject head;

    public float fireRateLow;
                            // theese numbers are both for how fast the ai will shoot
    public float fireRateHigh;

    public LayerMask playerLM;
    public int damage;
    
    AudioSource soruce;
    public AudioClip gunShot;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<NavMeshAgent>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        soruce = gameObject.GetComponent<AudioSource>();
        soruce.volume = Object.FindObjectOfType<SettingsManager>().audioLevel;
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
                Vector3 offset;
                hasShot = true;
                GameObject spawnedBullet = Instantiate(bullet, muzzlePoint.transform); // create a variable of the spawned object so we can point it at the player
                spawnedBullet.gameObject.GetComponent<Damager>().ModifyDamage(damage);
                Invoke("ResetShoot", Random.Range(fireRateLow, fireRateHigh)); // randomise the fire rate a bit, so all the enemies dont shoot uniformailly
                if(useInnacuracy && Vector3.Distance(gameObject.transform.position, player.transform.position) >= 5) // randomly change the target in the xy directon to add some innacruacy so that the enemies dont hit the player every time
                {                                                                                                    // do this if the distane between the enemy and the player is more then 5
                     offset = new Vector3(player.transform.position.x - Random.Range(-bulletSpred, bulletSpred), player.transform.position.y, (player.transform.position.z - Random.Range(-bulletSpred, bulletSpred)));
                }
                else
                {
                    offset = player.transform.position; // if not ussing the innacuracy or if the enemy is close to the player, just set the hit point directorly on the player
                }
                spawnedBullet.transform.LookAt(offset);
                soruce.PlayOneShot(gunShot);
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
