using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Run in Editor
[ExecuteInEditMode]
public class AdjustTileMap : MonoBehaviour
{
    // Store Tile Map
    public GameObject tileMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // Stop here if Playing in Editor
        if (Application.isPlaying)
        {
            return;
        }
        GridSystem gridSystemComponent = GetComponent<GridSystem>();
        // Shift tileMap and adjust cell size to custom grid system
        tileMap.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (gridSystemComponent.gridPointDistance / 2));
        tileMap.GetComponent<Grid>().cellSize = new Vector3(gridSystemComponent.gridPointDistance, gridSystemComponent.gridPointDistance, 0);
        // Loop through tileMap childrent and adjust scales to fit cells
        int children = tileMap.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            // Skip 0 index
            if (i != 0)
            {
                tileMap.transform.GetChild(i).transform.localScale = new Vector3(gridSystemComponent.gridPointDistance, 1, gridSystemComponent.gridPointDistance);
            }
        }
    }
}
