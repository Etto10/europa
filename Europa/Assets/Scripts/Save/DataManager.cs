using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public ItemDB ItemDB;


    [Header("PREFABS")]
    public List<PrefabItem> prefabItems = new();

    [Header("Parents")]
    [SerializeField] private Transform metalPlateP, oxygenGeneratorP, plasticSheetP, seedP, soilP; //All the different parents


    private string path;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        string slot = PlayerPrefs.GetInt("slot").ToString();
        path = Application.persistentDataPath + "/SaveSlot" + slot + "/europa.xml";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveSlot" + slot); // returns a DirectoryInfo object
        }
        StartCoroutine(SaveCoroutine());
    }

    public void AddItem(GameObject obj)
    {
        Item item = new();
        item.prefab_id = GridManager.Instance.itemData.id;
        item.item_id = item.prefab_id + ItemDB.items.Count.ToString() + Random.Range(0f, 22f).ToString();
        obj.name = item.item_id;
        item.position = obj.transform.position;
        ItemDB.items.Add(item);
    }

    public void AddTile(GameObject obj)
    {
        TileItem item = new();
        Tile tile = obj.GetComponent<Tile>();
        item.canPlace = tile.canPlace;
        item.isTherePlate = tile.isTherePlate;
        item.isThereSoil = tile.isThereSoil;
        item.itemName = tile.tileId;

        ItemDB.tiles.Add(item);
    }


    public void SaveData()
    {
        XmlSerializer xmlSerializer = new(typeof(ItemDB));
        FileStream stream = new(path, FileMode.Create);
        xmlSerializer.Serialize(stream, ItemDB);
        stream.Close();

        DayCycle.Instance.Save();
        PlayerStats.Instance.Save();
    }

    bool firstLoad = true;

    public void LoadData()
    {
        if (!File.Exists(path))
        {
            SaveData();
            return;
        }

        XmlSerializer xmlSerializer = new(typeof(ItemDB));

        FileStream stream = new(path, FileMode.Open);

        ItemDB = xmlSerializer.Deserialize(stream) as ItemDB;
        stream.Close();

        foreach (Item item in ItemDB.items)
        {
            GameObject requestedPrefab = RequestPrefab(item.prefab_id);

            GameObject go = Instantiate(requestedPrefab, item.position, Quaternion.identity);

            switch (item.item_id)
            {
                case "metal_plate":
                    go.transform.SetParent(metalPlateP);
                    break;
                case "oxygen_generator":
                    go.transform.SetParent(oxygenGeneratorP);
                    break;
                case "plastic_sheet":
                    go.transform.SetParent(plasticSheetP);
                    break;
                case "seed":
                    go.transform.SetParent(seedP);
                    break;
                case "soil":
                    go.transform.SetParent(soilP);
                    break;
            }

            go.name = item.item_id;
        }

        if(ItemDB.tiles.Count > 0)
        {
            foreach (TileItem item in ItemDB.tiles)
            {
                GameObject go = GameObject.Find(item.itemName);
                Tile tile = go.GetComponent<Tile>();
                tile.canPlace = item.canPlace;
                tile.isTherePlate = item.isTherePlate;
                tile.isThereSoil = item.isThereSoil;
            }
        }

        if (firstLoad)
        {
            firstLoad = false;
            GridManager.Instance.HideGrid();
        }
    }

    IEnumerator SaveCoroutine()
    {
        yield return new WaitForSeconds(10f);
        SaveData();
        StartCoroutine(SaveCoroutine());
    }

    public GameObject RequestPrefab(string id)
    {
        PrefabItem item_ = new();
        foreach (PrefabItem item in prefabItems)
        {
            if (item.id == id)
            {
                item_ = item;
            }
        }

        return item_.prefab;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}


[System.Serializable]
public class ItemDB
{
    public List<Item> items = new();
    public List<TileItem> tiles = new();
}

[System.Serializable]
public class Item
{
    public string prefab_id;
    public string item_id;
    public Vector3 position;
}

[System.Serializable]
public class TileItem
{
    public bool canPlace, isTherePlate, isThereSoil;
    public string itemName;
}


[System.Serializable]
public class PrefabItem
{
    public GameObject prefab;
    public string id;
}