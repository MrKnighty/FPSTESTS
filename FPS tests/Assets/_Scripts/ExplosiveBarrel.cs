using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float explosionRadius;
    public float damage;
    public GameObject explosionParticles;
    DamageHandeler DH;
    bool hit;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.transform.tag == "Bullet" && !hit)
        {
            hit = true; // this is used so that if the player shoots this with a shotgun, it does not get called multible times;
            Collider[] objects = Physics.OverlapSphere(this.transform.position, explosionRadius);
            foreach(Collider hits in objects)
            {
                if(DH = hits.gameObject.GetComponent<DamageHandeler>())
                {
                  print(hits);
                  hits.gameObject.GetComponent<DamageHandeler>().DoDamage(damage); 
                }
                 
                
                
            }

            GameObject particles = Instantiate(explosionParticles, transform);
            particles.transform.parent = null;
            print("hit");
            Destroy(gameObject);
            
        }
        





    }
}
