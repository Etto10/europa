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
        gameObject.SetActive(false);
    }
}
