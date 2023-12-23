using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlacementEditorWindow : EditorWindow
{
    private Texture2D noiseMapTexture;
    private float density = 0.5f;
    private GameObject prefab;
    private float maxHeight = 100;
    private float maxSteepness = 30;

    [MenuItem("Tools/Wizards Code/Tutorial/Plant Placement")]
    public static void ShowWindow()
    {
        GetWindow<PlacementEditorWindow>("Plant Placement");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        noiseMapTexture = (Texture2D)EditorGUILayout.ObjectField("Noise Map Texture", noiseMapTexture, typeof(Texture2D), false);
        if (GUILayout.Button("Generate Noise"))
        {
            int width = (int)Terrain.activeTerrain.terrainData.size.x;
            int height = (int)Terrain.activeTerrain.terrainData.size.z;
            float scale = 5;
            noiseMapTexture = Noise.GetNoiseMap(width, height, scale);
        }
        EditorGUILayout.EndHorizontal();

        maxHeight = EditorGUILayout.Slider("Max Height", maxHeight, 0, 1000);
        maxSteepness = EditorGUILayout.Slider("Max Steepness", maxSteepness, 0, 90);
        density = EditorGUILayout.Slider("Density", density, 0, 1);

        prefab = (GameObject)EditorGUILayout.ObjectField("Object Prefab", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Place Objects"))
        {
            PlaceObjects(Terrain.activeTerrain, noiseMapTexture, maxHeight, maxSteepness, density, prefab);
        }

    }

    public static void PlaceObjects(Terrain terrain, Texture2D noiseMapTexture, float maxHeight, float maxSteepness, float density, GameObject prefab)
    {
        Transform parent = new GameObject("PlacedObjects").transform;

        for (int x = 0; x < terrain.terrainData.size.x; x++)
        {
            for (int z = 0; z < terrain.terrainData.size.z; z++)
            {
            

                // If the value is above the threshold, instantiate a plant prefab at this location
                if (Fitness(terrain, noiseMapTexture, maxHeight, maxSteepness, x, z) > 1 - density)
                {
                    Vector3 pos = new Vector3(x + Random.Range(-0.5f, 0.5f), 0, z + Random.Range(-0.5f, 0.5f));
                    pos.y = terrain.SampleHeight(new Vector3(x, 0f, z));

                    GameObject go = Instantiate(prefab, pos, Quaternion.identity);
                    go.transform.SetParent(parent);
                }
            }
        }
    }

    private static float Fitness(Terrain terrain, Texture2D noiseMapTexture, float maxHeight, float maxSteepness, int x, int z)
    {
        float fitness = noiseMapTexture.GetPixel(x, z).g;

        fitness += Random.Range(-0.15f, 0.15f);
        //float steepness = terrain.terrainData.GetSteepness(x / terrain.terrainData.size.x, z / terrain.terrainData.size.z);
        //if (steepness > maxSteepness)
        //{
        //    fitness -= 0.7f;
        //}

        //float height = terrain.terrainData.GetHeight(x, z);
        //if (height > maxHeight)
        //{
        //    fitness -= 0.7f;
        //}

        return fitness;
    }
}
