using UnityEngine;
using System.Collections;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed,
                                            float scale, int octaves,
                                            float persistence, float lacunarity,
                                            Vector2 offset) {

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        float maxHeight = 0;
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < octaves; i++) {
            octaveOffsets[i] = new Vector2(prng.Next(-100000, 100000) + offset.x,
                                            prng.Next(-100000, 100000) - offset.y);

            maxHeight += amplitude;
            amplitude *= persistence;
        }


        if (scale <= 0) {
            scale = 0.00001f;
        }

        float[,] noiseMap = new float[mapWidth, mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++) {
                amplitude = 1;
                frequency = 1;
                float noiseHeight = 1;

                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x-mapWidth/2 + octaveOffsets[i].x) / scale * frequency ;
                    float sampleY = (y-mapHeight/2 + octaveOffsets[i].y) / scale * frequency ;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                noiseMap[x, y] = noiseHeight;

            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.Clamp(
                    (noiseMap[x, y] + 1) / (maxHeight * 2f),
                    0, float.MaxValue);

            }
        }

        return noiseMap;
    }
}
