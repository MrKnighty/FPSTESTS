using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopup : MonoBehaviour
{
    
    Text popupText;
    public IEnumerator UpdateText(string text)
    {
        popupText = gameObject.GetComponent<Text>();
        popupText.text = text;
        yield return new WaitForSeconds(0.5f);
        popupText.text = "";
    }
}
