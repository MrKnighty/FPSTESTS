using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
  GameObject highlighted;
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50f))
        {
           if (highlighted != null) highlighted.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
           highlighted = hit.transform.gameObject;
           highlighted.GetComponent<SpriteRenderer>().color = Color.green;

        }
    }
}
