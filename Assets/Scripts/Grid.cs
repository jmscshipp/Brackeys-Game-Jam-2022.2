using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int xSize, zSize; // set this in the inspector to make the grid dimensions
    public GameObject tilePrefab;

    void Start()
    {
        // instantiate grid based on chosen dimensions
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                Instantiate(tilePrefab, new Vector3(x, 0.0f, z), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        
    }
}
