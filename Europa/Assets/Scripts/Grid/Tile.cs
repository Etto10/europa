using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private GameObject highlight;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public string tileId;

    //[HideInInspector]
    public bool canPlace = false;

    public bool isTherePlate, isThereSoil = false;

    public bool _isOffset;
    public void Init(bool isOffset)
    {
        _isOffset = isOffset;
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }

    private void Start()
    {
        highlight.SetActive(false);
    }

    private void OnMouseOver()
    {
        highlight.SetActive(false);
        if (!canPlace)
            return;

        GridManager gridManager = GridManager.Instance;
        if (canPlace && !gridManager.checkPlate && !isTherePlate)
        {
            highlight.SetActive(true);
        }
        if (canPlace && gridManager.checkPlate && isTherePlate)
        {
            highlight.SetActive(true);
        }

        if(canPlace && gridManager.checkSoil && isThereSoil)
        {
            highlight.SetActive(true);
        }
        else if(canPlace && !gridManager.checkSoil && !isThereSoil)
        {
            highlight.SetActive(true);
        }

        if (!canPlace) //checking every case for items that require a plate
        {
            highlight.SetActive(false);
        }
        if (gridManager.checkPlate && !isTherePlate)
        {
            highlight.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (!canPlace)
            return;

        GridManager gridManager = GridManager.Instance;

        bool _canPlace = false;
        if (canPlace && !gridManager.checkPlate && !isTherePlate)
        {
            _canPlace = true;
        }
        else if (canPlace && gridManager.checkPlate && isTherePlate)
        {
            _canPlace = true;
        }

        if (canPlace && gridManager.checkSoil && isThereSoil)
        {
            _canPlace = true;
        }
        else if (canPlace && !gridManager.checkSoil && !isThereSoil)
        {
            _canPlace = true;
        }

        if (!canPlace) 
        {
            _canPlace = false;
            gridManager.HideGrid();
        }
        if(gridManager.checkPlate && !isTherePlate)
        {
            _canPlace = false;
            gridManager.HideGrid();
        }

        if(gridManager.checkSoil && !isThereSoil)
        {
            _canPlace = false;
            gridManager.HideGrid();
        }
        else if (!gridManager.checkSoil && isThereSoil)
        {
            _canPlace = false;
            gridManager.HideGrid();
        }

        if (_canPlace)
            PlaceItem();
    }

    private void PlaceItem()
    {
        InventoryItemData item = GridManager.Instance.itemData;


        GridManager.Instance.itemController.gameObject.GetComponent<InventoryItemController>().RemoveItem();

        GameObject go = Instantiate(item.prefab, transform.position, Quaternion.identity);
        go.transform.SetParent(GameObject.Find(item.id + "s").transform);

        GridManager.Instance.HideGrid();
        DataManager.Instance.AddItem(go);

        if (item.id == "metal_plate")
        {
            isTherePlate = true;
        }
        else if (item.id == "soil")
        {
            isThereSoil = true;
        }
        else if (item.id == "plastic_sheet")
        {
            canPlace = false;
        }
        else if (item.id == "oxygen_generator")
        {
            canPlace = false;
        }

        DataManager.Instance.AddTile(gameObject);

        GridManager.Instance.itemData = null;
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Structure"))
        {
            canPlace = false;
        }
    }
}
