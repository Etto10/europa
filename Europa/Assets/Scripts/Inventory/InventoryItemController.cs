using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public InventoryItemData inventoryItemData;

    [Header("References")]
    public TMP_Text nameTxt;
    public TMP_Text descriptionTxt;
    public Image itemIcon;
    public TMP_Text itemQuantity;
}
