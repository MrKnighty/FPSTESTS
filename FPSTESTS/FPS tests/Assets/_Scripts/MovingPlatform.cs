using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   public GameObject destination;
   public float speed;
   Vector3 startingPosition;
   GameObject player;
   bool parentedPlayer;

   private void Start() 
   {
       startingPosition = transform.position;
       player = GameObject.FindGameObjectWithTag("Player");

   }
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(destination.transform.position, startingPosition, time);

        if(Vector3.Distance(this.transform.position, player.transform.position) >= 5 && parentedPlayer) 
        {
            player.transform.parent = null; // this is a fail safe, since ontrigger exit does not allways trigger and the player gets  stuck parented to the object
            parentedPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player") other.transform.parent = this.transform;
        parentedPlayer = true;
        
    }
    private void OnTriggerExit(Collider other)  //parent the object to the moving platfrom, so that the object moves along with it;
    {
        if(other.tag == "Player") other.transform.parent = null;
        parentedPlayer = false;
        
    }
}
