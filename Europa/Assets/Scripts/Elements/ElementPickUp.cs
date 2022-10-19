using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementPickUp : MonoBehaviour
{
    public InventoryItemData item;
    public Element element;

    private void Pickup()
    {
        //Make sure that when you add the item you set everything
        InventoryManager.Instance.Add(item);
        InventoryManager.Instance.ListItems();

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
        if (!hasBeenPicked || InventoryManager.Instance.items.Count < InventoryManager.Instance.maxItems || item.itemType == InventoryItemData.ItemType.Item)
        {
            hasBeenPicked = true;
            Pickup();
        }
    }
}
