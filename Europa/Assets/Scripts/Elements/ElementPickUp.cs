using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementPickUp : MonoBehaviour
{
    public InventoryItemData item;
    [SerializeField] private InventoryItemData seed;
    [SerializeField] private InventoryItemData soil;
    public Element element;

    [SerializeField] private GameObject soilPrefab;

    private void Pickup()
    {
        //Add item to inventory


        if (item.id == "eberries" && seed != null && soil != null)
        {
            //Add a seed and two soils
        }
        else if(item.id == "ebberries" && (seed == null || soil == null))
        {
            Debug.Log("Seed or soil is null");
        }


        if (element != null)
        {
            int elementType = (int)element.elementType;
            List<bool> elementsFound = PlayerStats.Instance.elementsFound;
            if (!elementsFound[elementType])
            {
                elementsFound[elementType] = true;
                ElementManager.Instance.FoundItem(elementType);
                Debug.Log("Found " + element.elementType.ToString());
                AudioManager audio = AudioManager.Instance;
                audio.ProgressMusic();
                audio.Play("found_item");
            }

            //Replace obj with plain
            Vector2 pos = transform.position;
            Instantiate(PerlinNoiseMap.Instance.plainPrefab, pos, Quaternion.identity);
        }
        else
        {
            //Replace obj with soil
            Vector2 pos = transform.position;
            Instantiate(soilPrefab, pos, Quaternion.identity);
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    bool hasBeenPicked = false;

    private void Start()
    {
        hasBeenPicked = false;
    }

    private void OnMouseDown()
    {
        /*
        if (!hasBeenPicked || InventoryManager.Instance.items.Count < InventoryManager.Instance.maxItems || item.itemType == InventoryItemData.ItemType.Item)
        {
            hasBeenPicked = true;
            Pickup();
        }
        */
    }
}
