using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSnap : MonoBehaviour
{
    [Header("Game Object Must Use Capsule Collider")]

    // Size of sphere cast and what it's looking for
    public float castRadius;
    // Used to offset y position so objects snap above ground
    public float floorHeight;
    // LayerMasks for gathering info on specific objects
    public LayerMask avaliableGridPoint;
    public LayerMask Terrain;

    // Stores gridPoint that object is sitting on
    GameObject currentGridPoint = null;
    // Null var for calculating distance to center of sphere cast
    Transform minDistancePosition = null;

    #region Drag Positiong Methods Called From Game Manager
    public void DragObject()
    {
        // Cast ray from camera and return first object hit that has terrain layer assigned
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if hit true
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, Terrain)) 
        { 
            // Get Y Offset
            float yCoord = hit.point.y + GetComponent<CapsuleCollider>().bounds.extents.y;
            // Position parent at hit x, floorHeight, and hit z
            transform.position = new Vector3(hit.point.x, floorHeight, hit.point.z);
            // Position child at hit x, y + offset, and z
            transform.GetChild(0).position = new Vector3(hit.point.x, yCoord, hit.point.z);
        }
        // if hit false
        else
        {
            // Add z offset
            float zCoord = Camera.main.WorldToScreenPoint(transform.position).z + GetComponent<CapsuleCollider>().bounds.extents.y;
            // Get exact mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zCoord));
            // Positioning
            transform.position = new Vector3(mousePos.x, floorHeight, mousePos.z);
            transform.GetChild(0).position = transform.position;
        }
    }

    public void GridSnap()
    {
        // Reset vars
        minDistancePosition = null;
        // Check if object on gridPoint
        if (currentGridPoint != null)
        {
            // Change Layer to available so other objects can be dragged onto it
            currentGridPoint.layer = 6;
            currentGridPoint = null;
        }

        // Return array of colliders on target layer hit by sphere cast, cast on same value as gridpoints by subtracting floor height and subtract 1 to offset transform.up direction
        RaycastHit[] hits = Physics.SphereCastAll(new Vector3(transform.position.x, (transform.position.y - floorHeight) - 1, transform.position.z),
            castRadius, transform.up, castRadius / 2, avaliableGridPoint);

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

    public void AdjustModelHeightOnRelease()
    {
        // Cast ray from top of capsule model
        Ray ray = new Ray(new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + GetComponent<CapsuleCollider>().bounds.extents.y,
            transform.GetChild(0).position.z), Vector3.down);
        RaycastHit hit;
        // Check if ray hit terrain layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, Terrain))
        {
            // Sit on y just below capsule
            float capsuleDist = GetComponent<CapsuleCollider>().bounds.extents.y * 2;
            if (hit.distance > capsuleDist + 0.1)
            {
                transform.GetChild(0).position -= new Vector3(0, hit.distance - capsuleDist, 0);
            }
            else if (hit.distance < capsuleDist + 0.1)
            {
                transform.GetChild(0).position += new Vector3(0, capsuleDist - hit.distance, 0);
            }
        }
        // if no terrain object found
        else
        {
            // Reset child position
            transform.GetChild(0).position = transform.position;
        }
    }
    #endregion

    #region Archived Math that may be usable with enemy units
    //private void AdjustModelHeightOnDrag()
    //{
    //    if (followCollider)
    //    {
    //        // Cast ray from top of capsule model
    //        Ray ray = new Ray(new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + GetComponent<CapsuleCollider>().bounds.extents.y,
    //            transform.GetChild(0).position.z), Vector3.down);
    //        RaycastHit hit;
    //        // Check if ray hit terrain layer
    //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, Terrain))
    //        {
    //            // Add or subtract y value depending on distance so that model glides across surface
    //            float capsuleDist = GetComponent<CapsuleCollider>().bounds.extents.y * 2;
    //            if (hit.distance > capsuleDist + 0.3)
    //            {
    //                transform.GetChild(0).position -= new Vector3(0, 0.1f, 0);
    //            }
    //            else if (hit.distance < capsuleDist + 0.1)
    //            {
    //                transform.GetChild(0).position += new Vector3(0, 0.1f, 0);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        transform.GetChild(0).position = transform.position;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == 2)
    //    {
    //        followCollider = true;
    //        transform.GetChild(0).position = new Vector3(transform.position.x, other.transform.GetChild(0).position.y + 
    //            transform.GetComponent<CapsuleCollider>().bounds.extents.y, transform.position.z);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == 2)
    //    {
    //        followCollider = false;
    //    }
    //}
    #endregion
}
