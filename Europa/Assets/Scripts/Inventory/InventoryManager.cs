using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventoryItemData> items = new List<InventoryItemData>();

    [Header("References")]
    public Transform itemContent;
    public GameObject inventoryItem;

    [Header("Item List")]
    public InventoryItemController[] inventoryItems;

    [Header("Inventory Settings")]
    public int maxItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(InventoryItemData item)
    {
        if (itemContent.childCount >= maxItems)
            return;

        items.Add(item);
    }

    public void Remove(InventoryItemData item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        // Making sure that items don't multiply by destroying the previous elements
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        // Creating Inventory UI
        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            //var itemDesc = obj.transform.Find("ItemDescription").GetComponent<TMP_Text>();

            itemName.text = item.displayName;
            itemIcon.sprite = item.icon;
            //itemDesc.text = item.description;

        }

        SetInventoryItems();
    }

    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }
}
