using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGizmo : MonoBehaviour
{
    [SerializeField]
    private float gridPointSphereRadius = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, gridPointSphereRadius);
    }
}
