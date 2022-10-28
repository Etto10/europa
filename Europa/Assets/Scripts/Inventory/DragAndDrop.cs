using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;

    public bool dropOnSpaceship;

    private Canvas canvas;
    private Transform inventory;
    private Transform spaceship;

    private InventoryItemController controller;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        controller = GetComponent<InventoryItemController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        dropOnSpaceship = controller.FindDistance();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventory = GameObject.Find("InvContent").transform;
        spaceship = GameObject.Find("SpcContent").transform; 


        if (!dropOnSpaceship)
        {
            transform.SetParent(inventory, false);
            InventoryItemData item = GetComponent<InventoryItemController>().item;
            InventoryManager.Instance.Add(item);
            Chest.Instance.Remove(item);
        }
        else
        {
            transform.SetParent(spaceship, false);
            InventoryItemData item = GetComponent<InventoryItemController>().item;
            Chest.Instance.Add(item);
            InventoryManager.Instance.Remove(item);
        }
    }

}
