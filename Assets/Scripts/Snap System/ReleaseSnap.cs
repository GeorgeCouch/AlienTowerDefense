using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSnap : MonoBehaviour
{
    [Header("Game Object Must Use Capsule Collider")]
    // Size of sphere cast and what it's looking for
    public float castRadius;
    public LayerMask targetLayer;

    // Used to offset y position so objects snap above ground
    public float floorHeight;

    // Stores gridPoint that object is sitting on
    GameObject currentGridPoint;

    // Every frame while dragging object
    private void OnMouseDrag()
    {
        // Used to acively adjust the z coordinate so object stays on mouse
        float zCoord = Camera.main.WorldToScreenPoint(transform.position).z + GetComponent<CapsuleCollider>().bounds.extents.y;
        // Get Mouse Position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zCoord));
        // Convert all except z position to mouse transform.position
        transform.position = new Vector3(mousePos.x, transform.position.y, mousePos.z);
    }

    private void OnMouseUp()
    {
        // Check if object on gridPoint
        if (currentGridPoint != null)
        {
            // Change Layer to available so other objects can be dragged onto it
            currentGridPoint.layer = 6;
        }
        // Return array of colliders on target layer hit by sphere cast, cast on same value as gridpoints by subtracting floor height and subtract 1 to offset transform.up direction
        RaycastHit[] hits = Physics.SphereCastAll(new Vector3(transform.position.x, (transform.position.y - floorHeight) - 1, transform.position.z),
            castRadius, transform.up, castRadius/2, targetLayer); 
        // Null var for calculating distance
        Transform minDistancePosition = null;
        // Loop through colliders found by sphere cast
        for (int i = 0; i < hits.Length; i++)
        {
            // if minDistancePosition exists
            if (minDistancePosition != null)
            {
                // Get distances of currentMinDistance and hits[i] to center of sphereCast
                float currMinDistance = Vector3.Distance(minDistancePosition.position, new Vector3(transform.position.x, transform.position.y - floorHeight, transform.position.z));
                float hitDistance = Vector3.Distance(hits[i].transform.position, new Vector3(transform.position.x, transform.position.y - floorHeight, transform.position.z));
                // If hits[i] is closer, redefine minDistancePosition as hits[i]
                if (hitDistance < currMinDistance)
                {
                    minDistancePosition = hits[i].transform;
                    // get new gridPoint that object might move to
                    currentGridPoint = hits[i].transform.gameObject;
                }
            }
            // if minDistancePosition does not exist (first loop)
            else
            {
                minDistancePosition = hits[i].transform;
                currentGridPoint = hits[i].transform.gameObject;
            }
        }
        // Move object to closest gridPoint and position above ground
        transform.position = new Vector3(minDistancePosition.position.x, minDistancePosition.position.y + floorHeight, minDistancePosition.position.z);
        // Change Layer to occupied so no other objects can be dragged onto it
        currentGridPoint.layer = 7;
    }
}
