using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chest : MonoBehaviour
{
    public static Chest Instance;
    public bool playerDistance;
    public List<InventoryItemData> itemsContained = new();
    

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
    }

    public void Add(InventoryItemData item)
    {
        itemsContained.Add(item);
    }

    public void Remove(InventoryItemData item)
    {
        itemsContained.Remove(item);
    }

    public void ListItems()
    {
        // Making sure that items don't multiply by destroying the previous elements
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        // Creating Inventory UI
        Debug.Log(itemsContained.Count);
        foreach (var item in itemsContained)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            //var itemDesc = obj.transform.Find("ItemDescription").GetComponent<TMP_Text>();

            itemName.text = item.displayName;
            itemIcon.sprite = item.icon;
            //itemDesc.text = item.description;

            InventoryItemController inventoryItemController = obj.GetComponent<InventoryItemController>();
            if (!inventoryItemController.hasToFindDistance)
            {
                inventoryItemController.inventoryTransform = InventoryManager.Instance.inventoryTransform;
                inventoryItemController.spaceShipTransform = InventoryManager.Instance.spaceshipTransform;
                Debug.Log(inventoryItemController.item.id + " has to find distance");
                inventoryItemController.hasToFindDistance = true;
            }

        }


        SetInventoryItems();
    }

    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < itemsContained.Count; i++)
        {
            inventoryItems[i].AddItem(itemsContained[i]);
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
