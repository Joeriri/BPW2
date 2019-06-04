using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private Terrain terrain;

    [Header("Terrain")]
    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private int depth = 20;
    [SerializeField] private float scale = 20f;
    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float offsetY = 0f;

    [Header("Player")]
    [SerializeField] private Player player;
    private Vector2 playerTile = Vector2.zero;

    private void Awake()
    {
        terrain = GetComponent<Terrain>();
    }

    private void Start()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    private void Update()
    {

        if (Mathf.Floor(player.transform.position.x / width) != playerTile.x || Mathf.Floor(player.transform.position.z / height) != playerTile.y)
        {
            playerTile = new Vector2(Mathf.Floor(player.transform.position.x / width), Mathf.Floor(player.transform.position.z / height));
            transform.position = new Vector3(playerTile.x * width, 0, playerTile.y * height);
            offsetX = playerTile.x * width;
            offsetY = playerTile.x * height;
            terrain.terrainData = GenerateTerrain(terrain.terrainData);

            Debug.Log(playerTile);
        }
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    // Stop Coördinaten in perlin noise map
    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
