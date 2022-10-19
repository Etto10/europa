using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public Color baseColor, offsetColor;
    [SerializeField] private GameObject highlight;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //[HideInInspector]
    public bool canPlace;

    public bool isTherePlate, isThereSoil;

    private void Start()
    {
        isTherePlate = false;
        isThereSoil = false;
    }

    public bool _isOffset;
    public void Init(bool isOffset)
    {
        _isOffset = isOffset;
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }

    bool _active = false;
    private void Update()
    {
        if (highlight.activeInHierarchy && !_active)
        {
            _active = true;
        }
        else if(!highlight.activeInHierarchy && _active)
        {
            highlight.SetActive(false);
            _active = false;
        }
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
        else if (!gridManager.checkPlate && isTherePlate)
        {
            highlight.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (!canPlace)
            return;

        GridManager gridManager = GridManager.Instance;

        if (canPlace && !gridManager.checkPlate && !isTherePlate)
        {
            PlaceItem();
        }
        if (canPlace && gridManager.checkPlate && isTherePlate)
        {
            PlaceItem();
        }

        if (canPlace && gridManager.checkSoil && isThereSoil)
        {
            highlight.SetActive(true);
        }
        else if (canPlace && !gridManager.checkSoil && !isThereSoil)
        {
            highlight.SetActive(true);
        }

        if (!canPlace) 
        {
            gridManager.HideGrid();
        }
        if(gridManager.checkPlate && !isTherePlate)
        {
            gridManager.HideGrid();
        }
        else if (!gridManager.checkPlate && isTherePlate)
        {
            gridManager.HideGrid();
        }

        if(gridManager.checkSoil && !isThereSoil)
        {
            gridManager.HideGrid();
        }
        else if (!gridManager.checkSoil && isThereSoil)
        {
            gridManager.HideGrid();
        }
    }

    private void PlaceItem()
    {
        if (isTherePlate && GridManager.Instance.checkPlate || !isTherePlate && !GridManager.Instance.checkPlate)
        {
            InventoryItemData item = GridManager.Instance.itemData;

            GridManager.Instance.itemController.gameObject.GetComponent<InventoryItemController>().RemoveItem();

            GameObject go = Instantiate(item.prefab, transform.position, Quaternion.identity);
            go.transform.SetParent(GameObject.Find(item.id + "s").transform);

            GridManager.Instance.HideGrid();
            DataManager.Instance.AddItem(go);

            if(item.id == "metal_plate")
            {
                isTherePlate = true;
            }
            else if(item.id == "soil")
            {
                isThereSoil = true;
            }
            else if(item.id == "plastic_sheet")
            {
                canPlace = false;
            }

            GridManager.Instance.itemData = null;
        }
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
