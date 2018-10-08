using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndlessTerrain : MonoBehaviour
{

    const float updateThreshold = 25f;
    const float sqrUpdateThreshold = updateThreshold * updateThreshold;
    const float scale = 1f;

    public static float maxViewDistance;
    public LODInfo[] detailLevels;


    public Transform viewer;
    public static Vector2 viewerPostion;
    Vector2 oldViewerPosition;
    public Material mapMaterial;
    int chunkSize;
    int chunksVisible;

    static MapGenerator mapGenerator;

    Dictionary<Vector2, TerrainChunk> terrainDict = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> terrainChunksVisible = new List<TerrainChunk>();

    public void Start()
    {
        maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDistanceThreshold;
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunksVisible = Mathf.RoundToInt(maxViewDistance / chunkSize);
        mapGenerator = FindObjectOfType<MapGenerator>();

        UpdateVisibleChunks();
    }

    public void Update()
    {
        viewerPostion = new Vector2(viewer.position.x, viewer.position.z) / scale;
        if ((oldViewerPosition - viewerPostion).sqrMagnitude > sqrUpdateThreshold) {
            oldViewerPosition = viewerPostion;
            UpdateVisibleChunks();
        }
    }

    void UpdateVisibleChunks()
    {

        foreach (TerrainChunk chunk in terrainChunksVisible) {
            chunk.SetVisible(false);
        }

        terrainChunksVisible.Clear();

        int currentCoordX = Mathf.RoundToInt(viewerPostion.x / chunkSize);
        int currentCoordY = Mathf.RoundToInt(viewerPostion.y / chunkSize);

        for (int yOffset = -chunksVisible; yOffset <= chunksVisible; yOffset++)
        {
            for (int xOffset = -chunksVisible; xOffset <= chunksVisible; xOffset++)
            {
                Vector2 viewCoord = new Vector2(currentCoordX + xOffset, currentCoordY + yOffset);
                if (terrainDict.ContainsKey(viewCoord)) {
                    terrainDict[viewCoord].UpdateChunk();
                } else {
                    terrainDict.Add(viewCoord, new TerrainChunk(viewCoord, chunkSize, detailLevels, transform, mapMaterial));
                }

            }
        }
    }

    public class TerrainChunk {

        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        MapData mapData;
        bool mapDataRecieved;

        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;

        int prevLodIndex = -1;

        public TerrainChunk (Vector2 coord, int size, LODInfo[] detailLevels, Transform parent, Material material) {
            this.detailLevels = detailLevels;
            position = coord * size;

            bounds = new Bounds(position, Vector2.one * size);

            meshObject = new GameObject("Terrrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            PointLight pointLight = GameObject.Find("Point Light").GetComponent<PointLight>();
            // Update point light (sun)
            pointLight.Update();

            // Pass updated light positions to shader
            meshRenderer.material.SetColor("_PointLightColor", pointLight.color);
            meshRenderer.material.SetVector("_PointLightPosition", pointLight.GetWorldPosition());

            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = material;

            meshObject.transform.position = new Vector3(position.x, 0, position.y) * scale;
            meshObject.transform.localScale = Vector3.one * scale;
            meshObject.transform.parent = parent;

            SetVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];
            for (int i = 0; i < detailLevels.Length; i++) {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateChunk);
            }

            mapGenerator.RequestMapData(position, OnMapDataRecieved);

        }

        void OnMapDataRecieved(MapData mapData) {
            this.mapData = mapData;
            mapDataRecieved = true;

            meshRenderer.material.mainTexture =
                            TextureGenerator.TextureFromColorMap(mapData.colorMap, MapGenerator.mapChunkSize, MapGenerator.mapChunkSize);

            UpdateChunk();
        }

        public void UpdateChunk() {
            if (mapDataRecieved)
            {

                float viewerDistance = Mathf.Sqrt(bounds.SqrDistance(viewerPostion));
                bool visible = viewerDistance <= maxViewDistance
                    && position.y + scale * MapGenerator.mapChunkSize * scale / 2  > viewerPostion.y;

                if (visible)
                {
                    int lodIndex = 0;
                    for (int i = 0; i < detailLevels.Length - 1; i++)
                    {
                        if (viewerDistance > detailLevels[i].visibleDistanceThreshold)
                        {
                            lodIndex = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (prevLodIndex != lodIndex)
                    {
                        LODMesh lodM = lodMeshes[lodIndex];
                        if (lodM.recievedMesh)
                        {
                            prevLodIndex = lodIndex;
                            meshFilter.mesh = lodM.mesh;
                        }
                        else if (!lodM.requstedMesh)
                        {
                            lodM.RequestMesh(mapData);
                        }
                    }
                    terrainChunksVisible.Add(this);
                }

                SetVisible(visible);
            }
        }

        public void SetVisible(bool visible) {
            if (meshObject)
            {
                meshObject.SetActive(visible);
            }
        }


        public bool IsVisible() {
            return meshObject.activeSelf;
        }
    }

    class LODMesh {
        public Mesh mesh;
        public bool requstedMesh;
        public bool recievedMesh;
        public int lod;

        public Action updateCallback;

        public LODMesh (int lod, Action updateCallback) {
            this.lod = lod;
            this.updateCallback = updateCallback;
        }

        public void RequestMesh(MapData mapData) {
            requstedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, OnMeshDataRecieved);
        }

        void OnMeshDataRecieved(MeshData meshData) {
            mesh = meshData.CreateMesh();
            recievedMesh = true;
            updateCallback();
        }
    }

    [System.Serializable]
    public struct LODInfo {
        public int lod;
        public float visibleDistanceThreshold;
    }
}
