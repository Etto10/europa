using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    public static PerlinNoiseMap Instance;

    private Dictionary<int, GameObject> tileset;
    private Dictionary<int, GameObject> tile_groups;
    public GameObject plainPrefab, nitrogenPrefab, carbonPrefab, sulphurPrefab, phosphorusPrefab, slightHillPrefab, hillPrefab, ironPrefab;

    int map_width = 100;
    int map_height = 100;

    List<List<int>> noise_grid = new();
    List<List<GameObject>> tile_grid = new();

    // recommend 4 to 20
    float magnification = 5;

    [SerializeField] int x_offset = 0; // <- +>
    [SerializeField] int y_offset = 0; // v- +^


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Createtileset();
        CreateTileGroups();
        GenerateMap();
    }

    void CreateTileGroups()
    {
        /** Create empty gameobjects for grouping tiles of the same type, ie
            forest tiles **/

        tile_groups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefab_pair in tileset)
        {
            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(0, 0, 0);
            tile_groups.Add(prefab_pair.Key, tile_group);
        }
    }

    private void Createtileset()
    {
        tileset = new();
        tileset.Add(0, plainPrefab);
        tileset.Add(1, plainPrefab);
        tileset.Add(2, plainPrefab);
        tileset.Add(3, plainPrefab);
        tileset.Add(4, plainPrefab);
        tileset.Add(5, plainPrefab);
        tileset.Add(6, plainPrefab);
        tileset.Add(7, plainPrefab);
        tileset.Add(8, slightHillPrefab);
        tileset.Add(9, slightHillPrefab);
        tileset.Add(10, slightHillPrefab);
        tileset.Add(11, slightHillPrefab);
        tileset.Add(12, hillPrefab);
        tileset.Add(13, hillPrefab);
        tileset.Add(14, hillPrefab);
        tileset.Add(15, hillPrefab);
        tileset.Add(16, hillPrefab);
        tileset.Add(17, ironPrefab);
        tileset.Add(18, ironPrefab);
        tileset.Add(19, nitrogenPrefab);
        tileset.Add(20, carbonPrefab);
        tileset.Add(21, sulphurPrefab);
        tileset.Add(22, phosphorusPrefab);
    }

    private void GenerateMap()
    {
        /** Generate a 2D grid using the Perlin noise fuction, storing it as
            both raw ID values and tile gameobjects **/

        for (int x = 0; x < map_width; x++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());

            for (int y = 0; y < map_height; y++)
            {
                int tile_id = GetIdUsingPerlin(x, y);
                noise_grid[x].Add(tile_id);
                CreateTile(tile_id, x, y);
            }
        }
    }

    private int GetIdUsingPerlin(int x, int y)
    {
        /** Using a grid coordinate input, generate a Perlin noise value to be
            converted into a tile ID code. Rescale the normalised Perlin value
            to the number of tiles available. **/

        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification,
            (y - y_offset) / magnification
        );
        float clamp_perlin = Mathf.Clamp01(raw_perlin); // Thanks: youtu.be/qNZ-0-7WuS8&lc=UgyoLWkYZxyp1nNc4f94AaABAg
        float scaled_perlin = clamp_perlin * tileset.Count;

        // Replaced 4 with tileset.Count to make adding tiles easier
        if (scaled_perlin == tileset.Count)
        {
            scaled_perlin = (tileset.Count - 1);
        }
        return Mathf.FloorToInt(scaled_perlin);
    }

    private void CreateTile(int tile_id, int x, int y)
    {
        /** Creates a new tile using the type id code, group it with common
            tiles, set it's position and store the gameobject. **/

        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x * 5, y * 5, 0);

        tile_grid[x].Add(tile);
    }
}
