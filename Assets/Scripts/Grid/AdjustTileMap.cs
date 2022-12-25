using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Run in Editor
[ExecuteInEditMode]
public class AdjustTileMap : MonoBehaviour
{
    // Store Tile Map
    [SerializeField]
    private List<GameObject> tileMaps;

    // Update is called once per frame
    private void Update()
    {
        // Stop here if Playing in Editor
        if (Application.isPlaying)
        {
            return;
        }
        GridSystem gridSystemComponent = GetComponent<GridSystem>();
        for (int i = 0; i < tileMaps.Count; i++)
        {
            // Shift tileMap and adjust cell size to custom grid system
            tileMaps[i].transform.position = new Vector3(transform.position.x, tileMaps[i].transform.position.y, transform.position.z + (gridSystemComponent.getGridPointDistance() / 2));
            tileMaps[i].GetComponent<Grid>().cellSize = new Vector3(gridSystemComponent.getGridPointDistance(), gridSystemComponent.getGridPointDistance(), 0);
            // Loop through tileMap children and adjust scales to fit cells
            int children = tileMaps[i].transform.childCount;
            for (int j = 0; j < children; j++)
            {
                // Skip 0 index (tileMap object is first child)
                if (j != 0)
                {
                    tileMaps[i].transform.GetChild(j).transform.localScale = new Vector3(gridSystemComponent.getGridPointDistance(), 1, gridSystemComponent.getGridPointDistance());
                }
            }
        }
    }
}
