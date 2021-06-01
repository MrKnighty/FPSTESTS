using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float constantSpeed = 4f;
    public float despawnTime = 5f;
   

    private void Start()
    {
        Destroy(gameObject, despawnTime);
    }
    void Update()
    {
        transform.Translate(new Vector3(0, 0, constantSpeed * Time.deltaTime)); // give the bullet constant speed, we dont need to caculate more advanced bullet physics since the levels will be small
    }

    
}
