using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanger : MonoBehaviour
{
    // Start is called before the first frame update
  
    public string value;
   void OnEnable() 
   {
       gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat(value);
   }
}
