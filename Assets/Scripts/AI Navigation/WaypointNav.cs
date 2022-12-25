using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNav : MonoBehaviour
{
    public GameObject waypoints;
    public int i = 0;
    public float speed = 2;
    public LayerMask path;

    private void Update()
    {
        moveObjectAlongPath();
        AdjustHeightToFitPathHeight();
    }

    private void moveObjectAlongPath()
    {
        if (Vector3.Distance(transform.position, waypoints.transform.GetChild(i).position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints.transform.GetChild(i).position, speed * Time.deltaTime);
        }
        else
        {
            i++;
        }

        if (i >= waypoints.transform.childCount)
        {
            Destroy(gameObject);
        }
    }

    private void AdjustHeightToFitPathHeight()
    {
        // Cast ray from top of capsule collider
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + GetComponent<CapsuleCollider>().bounds.extents.y,
            transform.position.z), Vector3.down);
        RaycastHit hit;
        // Check if ray hit path layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, path))
        {
            // Add or subtract y value depending on distance so that model glides across surface
            float capsuleDist = transform.GetComponent<CapsuleCollider>().bounds.extents.y * 2;
            if (hit.distance > capsuleDist + 0.1)
            {
                transform.position -= new Vector3(0, hit.distance - capsuleDist, 0);
            }
            else if (hit.distance < capsuleDist + 0.1)
            {
                transform.position += new Vector3(0, capsuleDist - hit.distance, 0);
            }
        }
    }
}
