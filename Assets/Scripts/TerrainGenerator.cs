using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public int maxHeight = 1;
    public int width = 256;
    public int length = 256;

    public float scale = 1;

	// Use this for initialization
	void Start () {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}
	
    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, maxHeight, length);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights() {
        float[,] heights = new float[width, length];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                heights[x, y] = 
                    Mathf.PerlinNoise(x * scale / width, y * scale / width);
            }
        }

        return heights;
    } 

	// Update is called once per frame
	void Update () {
	}
}
