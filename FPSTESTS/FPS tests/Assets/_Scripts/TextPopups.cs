using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopups : MonoBehaviour
{
   public Text interactionText;
   public int popupDistance;
   public string message;
   bool textActive;
   
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
            textActive = true;
        }
        else if(textActive) // this will make sure this script isnt constantly clearing the text
        {
            interactionText.text = "";
            textActive = false;
        }
    }
}
