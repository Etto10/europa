using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItemController> itemsContained = new();
    private List<GameObject> UIItemsContained = new();

    [SerializeField] private GameObject inventoryItemPrefab;

    [SerializeField] private Transform inventoryContent;

    public void AddItem(InventoryItemData item)
    {
        bool foundMatch = false;
        for (int i = 0; i < itemsContained.Count; i++)
        {
            if(itemsContained[i].inventoryItemData.id == item.id)
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
            AddItemUI(newItemController) ;
        }
    }

    private void AddItemUI(InventoryItemController itemController)
    {
        InventoryItemData item = itemController.inventoryItemData;

        GameObject go = Instantiate(inventoryItemPrefab, inventoryContent);
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
