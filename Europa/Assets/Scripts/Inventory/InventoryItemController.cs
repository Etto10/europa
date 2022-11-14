using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItemData item;
    [HideInInspector] public Transform inventoryTransform;
    [HideInInspector] public Transform spaceShipTransform;
    [SerializeField] private GameObject descObj;
    [SerializeField] private TMP_Text descriptionTxt;


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
            case InventoryItemData.ItemType.Food:
                // Eat
                PlayerStats playerStats = PlayerStats.Instance;
                float max = playerStats.maxHunger;
                if (playerStats.hunger < max-20)
                {
                    playerStats.hunger += 20;
                }
                else if(playerStats.hunger < max)
                {
                    playerStats.hunger += max - playerStats.hunger;
                }
                Destroy(gameObject);
                break;

            default:
                break;
        }

    }

    

    float invDistance, spaceDistance;
    public bool FindDistance()
    {
        invDistance = Vector2.Distance(transform.position, inventoryTransform.position);
        spaceDistance = Vector2.Distance(transform.position, spaceShipTransform.position);

        if (invDistance < spaceDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }



    //DO THE CODE FOR THE DESCRIPTION APPEARING AND DISAPPEARING

    Color startColor;
    Image image;

    Color _startColor;
    TMP_Text text;
    private void Start()
    {
        InventoryManager inventoryManager = InventoryManager.Instance;
        inventoryTransform = inventoryManager.inventoryTransform;
        spaceShipTransform = inventoryManager.spaceshipTransform;

        descriptionTxt.text = item.description;

        image = descObj.GetComponent<Image>();
        startColor = image.color;
        image.color = Color.clear;

        text = descriptionTxt;
        _startColor = text.color;
        text.color = Color.clear;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.clear;
        text.color = Color.clear;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = startColor;
        text.color = _startColor;
    }
}
