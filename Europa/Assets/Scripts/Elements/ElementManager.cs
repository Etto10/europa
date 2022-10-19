using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    

    [SerializeField] private TMP_Text carbonTxt, nitrogenTxt, sulphurTxt, phosphorusTxt, ironTxt;
    private List<TMP_Text> elementTexts = new(); //0: Carbon, 1: Nitrogen, 2: Sulphur, 3: Phosphorus, 4: Iron

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
    public void FoundItem(int elementIndex)
    {
        elementTexts[elementIndex].color = foundColor;
    }
}
