using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loading : MonoBehaviour
{
    public TMP_Text loadingTxt;

    private IEnumerator Start()
    {
        loadingTxt.text = "Loading";
        yield return new WaitForSeconds(0.2f);
        loadingTxt.text = "Loading.";
        yield return new WaitForSeconds(0.2f);
        loadingTxt.text = "Loading..";
        yield return new WaitForSeconds(0.2f);
        loadingTxt.text = "Loading...";
        yield return new WaitForSeconds(0.2f);
        loadingTxt.text = "Loading";
        yield return new WaitForSeconds(0.2f);
        loadingTxt.text = "Loading.";
        yield return new WaitForSeconds(0.2f);
        loadingTxt.text = "Loading..";
        yield return new WaitForSeconds(0.2f);
        loadingTxt.text = "Loading...";
        yield return new WaitForSeconds(0.2f);

        InventoryManager.Instance.inventoryPanel.SetActive(false);
        Chest.Instance.chestItem.SetActive(false);
        GridManager.Instance.HideGrid();
        gameObject.SetActive(false);
    }
}