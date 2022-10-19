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

    private string path;
    private void Awake()
    {
        Instance = this;

        path = Application.dataPath + "/SaveFiles/europa.xml";

        LoadData();
    }

    private void Start()
    {
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


    public void SaveData()
    {
        XmlSerializer xmlSerializer = new(typeof(ItemDB));
        FileStream stream = new(path, FileMode.Create);
        xmlSerializer.Serialize(stream, ItemDB);
        stream.Close();
    }

    private void LoadData()
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
            go.name = item.item_id;
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
}


[System.Serializable]
public class ItemDB
{
    public List<Item> items = new();
}

[System.Serializable]
public class Item
{
    public string prefab_id;
    public string item_id;
    public Vector3 position;
}


[System.Serializable]
public class PrefabItem
{
    public GameObject prefab;
    public string id;
}