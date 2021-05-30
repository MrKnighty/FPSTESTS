using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   public GameObject destination;
   public float speed;
   Vector3 startingPosition;

   private void Start() 
   {
       startingPosition = transform.position;

   }
    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(destination.transform.position, startingPosition, time);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player") other.transform.parent = this.transform;
        
    }
    private void OnTriggerExit(Collider other)  //parent the object to the moving platfrom, so that the object moves along with it;
    {
        if(other.tag == "Player") other.transform.parent = null;
        
    }
}
