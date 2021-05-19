using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float explosionRadius;
    public float damage;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.transform.tag == "Bullet")
        {
            Collider[] objects = Physics.OverlapSphere(this.transform.position, explosionRadius);
            foreach(Collider hits in objects)
            {
                print(hits);
                  hits.gameObject.GetComponent<DamageHandeler>().DoDamage(damage);  
                
                
            }
        }
        





    }
}
