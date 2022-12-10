using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSnap : MonoBehaviour
{
    // Size of sphere cast and what it's looking for
    public float castRadius;
    public LayerMask targetLayer;

    // Stores gridPoint that object is sitting on
    GameObject currentGridPoint;

    // Every frame while dragging object
    private void OnMouseDrag()
    {
        // Get Mouse Position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        // Return array of colliders on target layer hit by sphere cast
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, castRadius, transform.up, castRadius/2, targetLayer);
        // Null var for calculating distance
        Transform minDistancePosition = null;
        // Loop through colliders found by sphere cast
        for (int i = 0; i < hits.Length; i++)
        {
            // if minDistancePosition exists
            if (minDistancePosition != null)
            {
                // Get distances of currentMinDistance and hits[i] to player 
                float currMinDistance = Vector3.Distance(minDistancePosition.position, transform.position);
                float hitDistance = Vector3.Distance(hits[i].transform.position, transform.position);
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
        // Move object to closest gridPoint
        transform.position = minDistancePosition.position;
        // Change Layer to occupied so no other objects can be dragged onto it
        currentGridPoint.layer = 7;
    }
}
