using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum ElementType
{
    Carbon,
    Nitrogen,
    Sulphur,
    Phosphorus,
    Iron,
    Terrain
}

public class ElementManager : MonoBehaviour
{
    public static ElementManager Instance;

    [SerializeField] private Color foundColor;

    [Header("Found Item Panel")]
    [SerializeField] private GameObject foundPanel;
    [SerializeField] private TMP_Text elementNameTxt, elementDescTxt;
    [SerializeField] private Image elementImg;
    [SerializeField] private List<InventoryItemData> inventoryItemDatas;

    [SerializeField] private TMP_Text carbonTxt, nitrogenTxt, sulphurTxt, phosphorusTxt, ironTxt;
    private List<TMP_Text> elementTexts = new(); //0: Carbon, 1: Nitrogen, 2: Sulphur, 3: Phosphorus, 4: Iron

    [Header("Editor Only")]
    [SerializeField] private bool[] elementsFound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foundPanel.SetActive(false);
        elementTexts.Add(carbonTxt);
        elementTexts.Add(nitrogenTxt);
        elementTexts.Add(sulphurTxt);
        elementTexts.Add(phosphorusTxt);
        elementTexts.Add(ironTxt);
    }

    /// <summary>
    /// 0: Carbon, 1: Nitrogen, 2: Sulphur, 3: Phosphorus, 4: Iron
    /// </summary>
    /// <param name="itemIndex"></param>
    ///

    public void FoundItem(int elementIndex)
    {
        elementTexts[elementIndex].color = foundColor;
        foundPanel.SetActive(true);

        elementNameTxt.text = "Found " + inventoryItemDatas[elementIndex].displayName + "!";
        elementDescTxt.text = inventoryItemDatas[elementIndex].description + "!";
        elementImg.sprite = inventoryItemDatas[elementIndex].icon;
        elementsFound[elementIndex] = true;
    }
}
