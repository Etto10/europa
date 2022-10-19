using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseItem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryItemController controller = transform.GetComponent<InventoryItemController>();
            controller.UseItem();
            GridManager.Instance.itemData = controller.item;
            GridManager.Instance.itemController = controller;
        }
    }
}
