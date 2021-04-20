using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTranslation : MonoBehaviour
{
    public Vector3 Direction = new Vector3(0, 0, 1);


    private void Update()
    {
        transform.Translate(Direction * Time.deltaTime);
    }
}
