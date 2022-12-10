using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPointHolderGizmo : MonoBehaviour
{
    public float holderGizmoSphereRadius = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, holderGizmoSphereRadius);
    }
}
