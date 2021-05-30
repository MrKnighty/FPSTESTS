using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public GameObject[] enemies;

    public bool animateObjects;
    public GameObject[] objectsToAnimate;
}
public class NewWaveManager : MonoBehaviour
{
   [SerializeField] Waves[] waves;
   public void StartWave(int waveID)
   {
       foreach(GameObject enemies in waves[waveID].enemies)
       {
           enemies.SetActive(true);
       }

       if(waves[waveID].animateObjects)
       {
           foreach(GameObject objects in waves[waveID].objectsToAnimate)
           {
               objects.GetComponent<Animator>().Play("Start");
           }
       }
   }
}
