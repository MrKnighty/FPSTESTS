using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelTrigger : MonoBehaviour
{
    public bool loadNextLevel;
    public int Levelid;

    private void OnTriggerEnter(Collider other) 
 
    {
        if (other.tag == "Player")
        {
            if(loadNextLevel)  SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
            else SceneManager.LoadScene (Levelid);
        }
        

    }
}
