using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSnapGizmo : MonoBehaviour
{
    public float gizmoRadius;
    public float floorHeight;
    bool dragging;

    // Draw Sphere on same y as grid points if object is being dragged
    private void OnDrawGizmos()
    {
        if (dragging)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, (transform.position.y - floorHeight) - 1, transform.position.z) + transform.up, gizmoRadius);
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
