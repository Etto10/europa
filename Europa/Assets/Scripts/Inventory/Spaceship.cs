using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public List<InventoryItemController> itemsContained = new();

    [Header("")]
    [SerializeField] private List<GameObject> UIItemsContained = new(); //Needs to be filled manually

    [Header("")]
    [SerializeField] private Transform spaceshipContent;
    [SerializeField] private GameObject inventoryItemPrefab;

    private void Start()
    {
        foreach (GameObject _gameObject in UIItemsContained)
        {
            AddItemToList(_gameObject.GetComponent<InventoryItemController>().inventoryItemData);
        }
    }

    public void AddItem(InventoryItemData item)
    {
        bool foundMatch = false;
        for (int i = 0; i < itemsContained.Count; i++)
        {
            if (itemsContained[i].inventoryItemData.id == item.id)
            {
                itemsContained[i].inventoryItemData.itemQuantity += 1;
                foundMatch = true;
                UpdateUI();
                break;
            }
        }

        if (!foundMatch)
        {
            InventoryItemController newItemController = new();
            newItemController.inventoryItemData = item;
            newItemController.inventoryItemData.itemQuantity = 1;
            itemsContained.Add(newItemController);
            AddItemUI(newItemController);
        }
    }

    private void AddItemToList(InventoryItemData item)
    {
        bool foundMatch = false;
        for (int i = 0; i < itemsContained.Count; i++)
        {
            if (itemsContained[i].inventoryItemData.id == item.id)
            {
                itemsContained[i].inventoryItemData.itemQuantity += 1;
                foundMatch = true;
                UpdateUI();
                break;
            }
        }

        if (!foundMatch)
        {
            InventoryItemController inventoryItemController = Instantiate(inventoryItemPrefab, spaceshipContent).GetComponent<InventoryItemController>();
            inventoryItemController.inventoryItemData = item;
            inventoryItemController.nameTxt.text = item.displayName;
            inventoryItemController.descriptionTxt.text = item.description;
            inventoryItemController.itemIcon.sprite = item.icon;
            inventoryItemController.itemQuantity.text = item.itemQuantity.ToString();
            UIItemsContained.Add(inventoryItemController.gameObject);
            itemsContained.Add(inventoryItemController);
        }

    }

    private void AddItemUI(InventoryItemController itemController)
    {
        InventoryItemData item = itemController.inventoryItemData;

        GameObject go = Instantiate(inventoryItemPrefab, spaceshipContent);
        InventoryItemController inventoryItemController = go.GetComponent<InventoryItemController>();

        inventoryItemController.inventoryItemData = item;
        inventoryItemController.nameTxt.text = item.displayName;
        inventoryItemController.descriptionTxt.text = item.description;
        inventoryItemController.itemIcon.sprite = item.icon;
        inventoryItemController.itemQuantity.text = item.itemQuantity.ToString();

        UIItemsContained.Add(go);
    }

    private void UpdateUI()
    {
        foreach (GameObject _gameObject in UIItemsContained)
        {
            Destroy(_gameObject);
        }

        foreach (InventoryItemController item in itemsContained)
        {
            AddItemUI(item);
        }
    }
}
