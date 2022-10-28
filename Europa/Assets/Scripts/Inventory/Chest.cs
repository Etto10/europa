using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chest : MonoBehaviour
{
    public static Chest Instance;
    public bool playerDistance;
    public List<InventoryItemData> items = new();
    

    [Header("References")]
    public Transform itemContent;
    public GameObject chestItem;
    [SerializeField] private GameObject inventoryItem;
    [SerializeField] private Transform chestContent;
   


    [Header("Item List")]
    public InventoryItemController[] inventoryItems;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < chestContent.childCount; i++)
        {
            Transform child = chestContent.GetChild(i);
            items.Add(child.GetComponent<InventoryItemController>().item);
        }
    }

    public void Add(InventoryItemData item)
    {
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
        Debug.Log(items.Count);
        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.displayName;
            itemIcon.sprite = item.icon;
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

    public void CloseChest()
    {
        chestItem.SetActive(false);
    }

    private void Update()
    {
        if (playerDistance && !GridManager.Instance.gridMode)
        {
            // Show keyboard prompt
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventoryItem.SetActive(true);
                chestItem.SetActive(true);
            }
        }
    }
}
