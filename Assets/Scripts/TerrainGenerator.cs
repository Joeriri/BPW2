using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SOURCE CODE: https://answers.unity.com/questions/734210/tiling-problem-with-perlin-generated-terrain-chunk.html

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 mapSize = new Vector2(256, 256);
    [SerializeField] private float maxHeight = 15f;
    [SerializeField] private float scale = 5f;
    [SerializeField] private Vector2 noiseOffset = new Vector2(100, 100);

    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    private Vector2 playerTile = Vector2.zero;

    void Start()
    {
        // Genereer de woestijn!
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                GenerateMesh(new Vector2(playerTile.x + x, playerTile.y + y));
            }
        }
    }

    void Update()
    {
        if (Mathf.Floor(player.transform.position.x / mapSize.x) != playerTile.x || Mathf.Floor(player.transform.position.z / mapSize.y) != playerTile.y)
        {
            // Update playerTile
            var oldPlayerTile = playerTile;
            playerTile = new Vector2(Mathf.Floor(player.transform.position.x / mapSize.x), Mathf.Floor(player.transform.position.z / mapSize.y));
            var dirTile = playerTile - oldPlayerTile;

            // Destroy TerrainTiles die zich buiten een 3x3 grid om de speler bevinden.
            var tiles = FindObjectsOfType<TerrainTile>();
            foreach (TerrainTile terrainTile in tiles)
            {
                var localCoords = playerTile - terrainTile.tile;
                if (Mathf.Abs(localCoords.x) > 1 || Mathf.Abs(localCoords.y) > 1)
                {
                    Destroy(terrainTile.gameObject);
                }
            }

            // Maak nieuwe tiles aan in de richting die de speler heeft genomen.
            if (dirTile.x != 0)
            {
                for (int y = -1; y <= 1; y++)
                {
                    GenerateMesh(new Vector2(playerTile.x + dirTile.x, playerTile.y+ y));
                }
            }

            if (dirTile.y != 0)
            {
                for (int x = -1; x <= 1; x++)
                {
                    GenerateMesh(new Vector2(playerTile.x + x, playerTile.y + dirTile.y));
                }
            }
        }
    }

    void GenerateMesh(Vector2 coords)
    {
        int xOrg = (int)coords.x;
        int yOrg = (int)coords.y;

        float[,] Heights = new float[(int)mapSize.x + 1, (int)mapSize.y + 1];
        Heights = CalcNoise(xOrg, yOrg);
        TerrainData NewTerrain = new TerrainData();

        NewTerrain.heightmapResolution = (int)mapSize.x;
        NewTerrain.size = new Vector3(mapSize.x, maxHeight, mapSize.y);
        NewTerrain.baseMapResolution = (int)mapSize.x / 2;
        NewTerrain.SetDetailResolution(1024, 16);
        NewTerrain.SetHeights(0, 0, Heights);

        // Maak nieuwe terrain tile
        GameObject TerrainSquare = Terrain.CreateTerrainGameObject(NewTerrain);
        TerrainSquare.transform.position = new Vector3((xOrg * mapSize.x), 0, (yOrg * mapSize.y));

        // Voeg TerrainTile script toe en geef de tile de juiste coördinaten
        TerrainSquare.AddComponent<TerrainTile>();
        TerrainSquare.GetComponent<TerrainTile>().tile = new Vector2(xOrg, yOrg);
    }
    
    // Genereer Height Data aan de hand van Perlin Noise
    float[,] CalcNoise(int xOrg, int yOrg)
    {
        float[,] finalheight = new float[(int)mapSize.x + 1, (int)mapSize.y + 1];

        float y = 0.0f;
        while (y < (int)mapSize.y + 1)
        {
            float x = 0.0f;
            while (x < (int)mapSize.x + 1)
            {
                float Xcoord = xOrg * mapSize.x + (x - 1);
                float Ycoord = yOrg * mapSize.y + (y - 1);
                float ColorSample = PerlinNoise(Xcoord, Ycoord);

                finalheight[(int)y, (int)x] = 0 + (ColorSample);

                x++;
            }

            y++;
        }

        return finalheight;
    }

    // Zoek Perlin Noise aan de hand van coördinaten
    public float PerlinNoise(float x, float y)
    {
        float xCoord = x / (mapSize.x) * scale + noiseOffset.x;
        float yCoord = y / (mapSize.y) * scale + noiseOffset.y;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    // Destory de TerrainTile met de passende tile coördinaten
    void DestroyTile(Vector2 coords)
    {
        var tiles = FindObjectsOfType<TerrainTile>();

        foreach(TerrainTile terrainTile in tiles)
        {
            if (terrainTile.tile == coords)
            {
                Destroy(terrainTile.gameObject);
            }
        }
    }

    private TerrainTile FindTile(Vector2 coords)
    {
        var tiles = FindObjectsOfType<TerrainTile>();
        var tile = new TerrainTile();

        foreach (TerrainTile terrainTile in tiles)
        {
            if (terrainTile.tile == coords)
            {
                tile = terrainTile;
            }
        }

        return tile;
    }
}
