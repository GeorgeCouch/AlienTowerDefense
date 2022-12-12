using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPointGizmo : MonoBehaviour
{
    [SerializeField]
    private float gridPointSphereRadius = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gridPointSphereRadius);
    }
}
