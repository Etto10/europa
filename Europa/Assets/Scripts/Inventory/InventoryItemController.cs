using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public InventoryItemData item;
    private Transform inventoryTransform;
    private Transform spaceShipTransform;
    public GameObject descriptionTxt;


    private void Start()
    {
        inventoryTransform = GameObject.Find("InvPos").transform;
        spaceShipTransform = GameObject.Find("SsPos").transform;
    }

    private void Update()
    {
        FindDistance();
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(InventoryItemData newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        GridManager gridManager = GridManager.Instance;

        switch (item.itemType)
        {
            case InventoryItemData.ItemType.Item:
                // Useless 
                break;
            case InventoryItemData.ItemType.Placeable:
                // Show grid and create function place
                gridManager.ShowGrid();
                Chest.Instance.CloseChest();
                gridManager.checkPlate = false;
                gridManager.checkSoil = false;
                break;
            case InventoryItemData.ItemType.NeedsPlate:
                // Show grid and create function place
                gridManager.ShowGrid();
                Chest.Instance.CloseChest();
                // control if there is availble plate
                gridManager.checkPlate = true;
                gridManager.checkSoil = false;

                break;
            case InventoryItemData.ItemType.Seed:
                // Show grid and create function place AND control if there is availble soil
                gridManager.ShowGrid();
                Chest.Instance.CloseChest();
                gridManager.checkSoil = true;
                gridManager.checkPlate = false;

                break;
            case InventoryItemData.ItemType.Analyzer:
                //Gray out the screen and highlight structures
                break;
            case InventoryItemData.ItemType.Food:
                // Eat
                break;

            default:
                break;
        }

    }

    private void FindDistance()
    {
        if (Vector2.Distance(transform.position, inventoryTransform.position) < Vector2.Distance(transform.position, spaceShipTransform.position))
        {
            GetComponent<DragAndDrop>().dropOnSpaceship = false;
        }
        else
        {
            GetComponent<DragAndDrop>().dropOnSpaceship = true;
        }
    }

    

    //DO THE CODE FOR THE DESCRIPTION APPEARING AND DISAPPEARING
}
