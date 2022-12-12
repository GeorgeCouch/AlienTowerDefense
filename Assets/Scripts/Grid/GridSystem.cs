using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    // Grid Point GameObject for spawning
    [SerializeField]
    private GameObject gridPoint;

    // Parameters for how many to spawn and the distance between them (Only works if Width even and Height odd)
    [Header("Width: Even, Height: Odd")]
    [SerializeField]
    private int gridWidth;
    [SerializeField]
    private int gridHeight;
    [SerializeField]
    private float gridPointDistance;

    // Holds position for where to spawn next gridPoint
    private Vector3 spawnLocation;

    // stores origin position
    private Vector3 originSpawnLocation;

    // Start is called before the first frame update
    private void Start()
    {
        spawnLocation = transform.position;
        // Offset Spawn origin to top left so that this transform will be in center
        spawnLocation.x -= ((gridWidth - 1) * gridPointDistance) / 2;
        spawnLocation.z -= ((gridHeight - 1) * gridPointDistance) / 2;
        // Store origin position
        originSpawnLocation = spawnLocation;
        SpawnGrid();
    }

    // Spawns grid based on width and height and adds offsets based on gridPointDistance
    private void SpawnGrid()
    {
        // Loop through height
        for (int i = 0; i < gridHeight; i++)
        {
            // If not on first row
            if (i != 0)
            {
                // Create height offset
                Vector3 zOffset = spawnLocation;
                zOffset.z += gridPointDistance;
                spawnLocation = zOffset;
                // Reset x position for next line of grid points
                spawnLocation.x = originSpawnLocation.x;
            }
            // Loop through width
            for (int j = 0; j < gridWidth; j++)
            {
                // Spawn GridPoint
                Instantiate(gridPoint, spawnLocation, transform.rotation, transform);
                // Create width offset
                Vector3 xOffset = spawnLocation;
                xOffset.x += gridPointDistance;
                spawnLocation = xOffset;
            }
        }
    }

    public float getGridPointDistance()
    {
        return gridPointDistance;
    }
}
