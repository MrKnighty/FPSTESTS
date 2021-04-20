using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;

    public Transform muzzlePoint; // where the bullets spawn

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, muzzlePoint.position, muzzlePoint.rotation);
        }
    }
}
