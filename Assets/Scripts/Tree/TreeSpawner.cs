using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{

    public GameObject treePrefab; // Drag and drop your tree prefab in the inspector
    public Terrain terrain; // Drag and drop your terrain in the inspector
    public int numberOfTrees = 10000; // Number of trees to be placed
    [SerializeField] private GameObject waterBorder;
    [SerializeField] private GameObject riverBorder;
    [SerializeField] private GameObject villageBorder;
    void Start()
    {
        PlaceTrees();

    }

    void PlaceTrees()
    {
        if (treePrefab == null || terrain == null || waterBorder == null)
        {
            Debug.LogError("Please assign the tree prefab and the terrain in the inspector.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        BoxCollider obstacleCollider = waterBorder.GetComponent<BoxCollider>();
        BoxCollider obstacleCollider2 = riverBorder.GetComponent<BoxCollider>();
        BoxCollider obstacleCollider3 = villageBorder.GetComponent<BoxCollider>();
        for (int i = 0; i < numberOfTrees; i++)
        {
            float randomX = Random.Range(0f, terrainSize.x);
            float randomZ = Random.Range(0f, terrainSize.z);
            float randomY = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

            // Adjust the height of trees slightly to avoid floating or sinking
            randomPosition.y += 0.1f;
            if (!IsInsideObstacle(randomPosition, obstacleCollider) && !IsInsideObstacle(randomPosition, obstacleCollider2) && !IsInsideObstacle(randomPosition, obstacleCollider3))
            {
                GameObject newTree = Instantiate(treePrefab, randomPosition, Quaternion.identity);
                newTree.transform.parent = gameObject.transform; // Optional: Organize trees under an empty game object
            }  
        }
    }

    bool IsInsideObstacle(Vector3 position, BoxCollider obstacleCollider)
    {
        // Check if the position is inside the box collider of the obstacle
        return obstacleCollider.bounds.Contains(position);
    }
}
