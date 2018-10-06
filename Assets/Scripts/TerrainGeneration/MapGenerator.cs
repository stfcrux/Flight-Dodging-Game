using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public const int mapChunkSize = 239;

    public float noiseScale;

    public int seed;
    public Vector2 offset;

    public float heightMultiplier;
    public AnimationCurve heightCurve;

    public int octaves;
    [Range(0, 1)]
    public float persistence;
    public float lacunarity;

    public TerrainTypes[] regions;

    Queue<MapThreadInfo<MapData>> mapThreadInfos = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshThreadInfos = new Queue<MapThreadInfo<MeshData>>();

    public void RequestMapData(Vector2 center, Action<MapData> callback) {
        ThreadStart threadStart = delegate {
            MapDataThread(center, callback);
        };

        new Thread(threadStart).Start();
    }

    void MapDataThread(Vector2 center, Action<MapData> callback) {
        MapData mapData = GenerateMapData(center);
        lock (mapThreadInfos)
        {
            mapThreadInfos.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }

    }

    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate {
            MeshDataThread(mapData, lod, callback);
        };

        new Thread(threadStart).Start();
    }

    void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback)
    {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, heightMultiplier, heightCurve, lod);

        lock (meshThreadInfos)
        {
            meshThreadInfos.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }

    }

    private void Update()
    {
        if (mapThreadInfos.Count > 0) {
            for (int i = 0; i < mapThreadInfos.Count; i++) {
                MapThreadInfo<MapData> threadInfo = mapThreadInfos.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
        if (meshThreadInfos.Count > 0)
        {
            for (int i = 0; i < meshThreadInfos.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshThreadInfos.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }

    MapData GenerateMapData(Vector2 center)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize+2, mapChunkSize+2, seed,
                                                   noiseScale, octaves,
                                                   persistence, lacunarity, center + offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];

        for (int x = 0; x < mapChunkSize; x++)
        {
            for (int y = 0; y < mapChunkSize; y++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight >= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                    } else {
                        break;
                    }
                }
            }
        }

        return new MapData(noiseMap, colorMap);
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }

    public struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public MapThreadInfo (Action<T> callback, T parameter) {
            this.callback = callback;
            this.parameter = parameter;
        }
    }
}


[System.Serializable]
public struct TerrainTypes
{
    public float height;
    public Color color;
    public string name;
}

public struct MapData {
    public readonly float[,] heightMap;
    public readonly Color[] colorMap;

    public MapData(float[,] heightMap, Color[] colorMap)
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}

