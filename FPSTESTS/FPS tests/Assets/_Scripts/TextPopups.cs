using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopups : MonoBehaviour
{
   public Text interactionText;
   public int popupDistance;
   public string message;
   
   GameObject player;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) <= popupDistance)
        {
            interactionText.text = message;
        }
        else
        {
            interactionText.text = "";
        }
    }
}
