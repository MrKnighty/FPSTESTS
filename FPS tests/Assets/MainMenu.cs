using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject[] menu;
    public void ChangeMenu(int menuID)
    {
        foreach(GameObject menus in menu)
        {
            menus.gameObject.SetActive(false);

        }

        menu[menuID].SetActive(true);
    }
    
    public void StartLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

}
