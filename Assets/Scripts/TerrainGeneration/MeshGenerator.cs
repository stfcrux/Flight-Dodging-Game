using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve _heightCurve, int levelOfDetail) {
        int increment = levelOfDetail == 0 ? 1 : levelOfDetail * 2;
        AnimationCurve heightCurve = new AnimationCurve(_heightCurve.keys);
        int borderedSize = heightMap.GetLength(0);
        int meshSize = borderedSize - 2*increment;
        int meshSizeMostDetail = borderedSize - 2;
        float topLeftX = (meshSizeMostDetail - 1) / -2f;
        float topLeftZ = (meshSizeMostDetail - 1) / 2f;

        int vertsPerLine = (meshSize - 1) / increment + 1;

        MeshData meshData = new MeshData(vertsPerLine);
        int[,] vertexMap = new int[borderedSize, borderedSize];
        int meshIndex = 0;
        int borderedIndex = -1;

        for (int y = 0; y < borderedSize; y += increment)
        {
            for (int x = 0; x < borderedSize; x += increment)
            {
                if (y == 0 || y == borderedSize - 1 || x == 0 || x == borderedSize-1) {
                    vertexMap[x, y] = borderedIndex;
                    borderedIndex--;
                } else {
                    vertexMap[x, y] = meshIndex;
                    meshIndex++;
                }

            }
        }
        for (int y = 0; y < borderedSize; y += increment) {
            for (int x = 0; x < borderedSize; x += increment) {
                int vertexIndex = vertexMap[x, y];

                Vector2 percent = new Vector2(((float)(x - increment)) / meshSize, ((float)(y - increment)) / meshSize);
                float height = heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier;
                Vector3 vertPos = new Vector3(topLeftX + percent.x * meshSizeMostDetail, height, topLeftZ - percent.y * meshSizeMostDetail);

                meshData.AddVertex(vertPos, percent, vertexIndex);


                if (x < borderedSize - 1 && y < borderedSize - 1) {
                    int a = vertexMap[x, y];
                    int b = vertexMap[x+increment, y];
                    int c = vertexMap[x, y+increment];
                    int d = vertexMap[x+increment, y+increment];
                    meshData.AddTriangle(a,d,c);
                    meshData.AddTriangle(d,a,b);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }

}

public class MeshData {
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    Vector3[] borderVertices;
    int[] borderTraingles;

    int triangleIndex;
    int borderTriangleIndex;

    public MeshData(int size) {
        vertices = new Vector3[size * size];
        uvs = new Vector2[size * size];
        triangles = new int[(size - 1) * (size - 1) * 6];

        borderVertices = new Vector3[size * 4 + 4];
        borderTraingles = new int[24 * size];
    }

    public void  AddVertex(Vector3 vertex, Vector2 uv, int index) {
        if (index < 0) {
            borderVertices[-index - 1] = vertex;
        } else {
            vertices[index] = vertex;
            uvs[index] = uv;
        }
    }

    public void AddTriangle(int a, int b, int c)
    {
        if (a < 0 || b < 0 || c < 0)
        {
            borderTraingles[borderTriangleIndex++] = a;
            borderTraingles[borderTriangleIndex++] = b;
            borderTraingles[borderTriangleIndex++] = c;
        } else {
            triangles[triangleIndex++] = a;
            triangles[triangleIndex++] = b;
            triangles[triangleIndex++] = c;
        }
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs
        };
        mesh.normals = CalculateNormals();
        return mesh;
    }


    Vector3[] CalculateNormals() {
        Vector3[] noramls = new Vector3[vertices.Length];
        int triangleCount = triangles.Length / 3;
        for (int i = 0; i < triangleCount; i++) {
            int normIndex = i * 3;
            int a = triangles[normIndex];
            int b = triangles[normIndex+1];
            int c = triangles[normIndex+2];
            Vector3 av = vertices[a];
            Vector3 bv = vertices[b];
            Vector3 cv = vertices[c];
            Vector3 triangleNormal = Vector3.Cross(bv - av, cv - av).normalized;
            noramls[a] += triangleNormal;
            noramls[b] += triangleNormal;
            noramls[c] += triangleNormal;
        }
        triangleCount = borderTraingles.Length / 3;
        for (int i = 0; i < triangleCount; i++)
        {
            int normIndex = i * 3;
            int a = borderTraingles[normIndex];
            int b = borderTraingles[normIndex + 1];
            int c = borderTraingles[normIndex + 2];
            Vector3 av = (a < 0) ? borderVertices[-a-1] : vertices[a];
            Vector3 bv = (b < 0) ? borderVertices[-b - 1] : vertices[b];
            Vector3 cv = (c < 0) ? borderVertices[-c - 1] : vertices[c];
            Vector3 triangleNormal = Vector3.Cross(bv - av, cv - av).normalized;
            if (a >= 0)
            {
                noramls[a] += triangleNormal;
            }
            if (b >= 0) {
                noramls[b] += triangleNormal;
            }
            if (c >= 0) {
                noramls[c] += triangleNormal;
            }
        }

        for (int i = 0; i < noramls.Length; i++) {
            noramls[i].Normalize();
        }

        return noramls;
    }
}
