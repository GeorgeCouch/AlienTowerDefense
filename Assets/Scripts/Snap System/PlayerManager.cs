using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Make Singleton
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    // Store unit layer and game object
    [SerializeField]
    private LayerMask draggable;
    private GameObject draggingUnit;

    // Bool based on if unit is clicked
    private bool unitBeingDragged;

    private void Update()
    {
        HandleUnitDragging();
    }

    // Manage Dragging Units
    private void HandleUnitDragging()
    {
        // On frame mouse down
        if (Input.GetMouseButtonDown(0))
        {
            // Cast ray from camera and search for draggable objects
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, draggable))
            {
                // Store draggable object and set bool if found
                draggingUnit = hit.transform.parent.gameObject;
                unitBeingDragged = true;
            }
        }

        // Call dragObject() from unit if being dragged and object stored
        if (unitBeingDragged && draggingUnit != null)
        {
            draggingUnit.GetComponent<ReleaseSnap>().DragObject();
        }

        // On frame mouse up
        if (Input.GetMouseButtonUp(0))
        {
            // Check if unit stored
            if (draggingUnit != null)
            {
                // Set bool and call positiong functions from unit
                unitBeingDragged = false;
                draggingUnit.GetComponent<ReleaseSnap>().GridSnap();
                draggingUnit.GetComponent<ReleaseSnap>().AdjustModelHeightOnRelease();
                // No longer storing unit
                draggingUnit = null;
            }
        }
    }
}
