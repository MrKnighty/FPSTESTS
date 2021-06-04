using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float grabDistance;
    GameObject grabbedObject;
   private void Update() 
   {
       
       if(Input.GetMouseButton(1))
       {
           RaycastHit hit;
           if(Physics.Raycast(gameObject.transform.position, transform.forward, out hit, grabDistance))
           {
               if(hit.transform.tag == "Grabbable")
               {
                   hit.transform.parent = this.transform;
               }
               if(Input.GetMouseButtonUp(1))
               {
                   hit.transform.parent = null;
               }
               
           }
       }
       




   }
}
