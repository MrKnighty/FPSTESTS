using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomLocationGIzmo : MonoBehaviour
{
    public Color color;
    private void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
