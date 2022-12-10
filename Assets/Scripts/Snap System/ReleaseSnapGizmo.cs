using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSnapGizmo : MonoBehaviour
{
    public float gizmoRadius;
    bool dragging;

    // Draw Sphere if object is being dragged
    private void OnDrawGizmos()
    {
        if (dragging)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        }
    }

    private void OnMouseDrag()
    {
        dragging = true;   
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}
