using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void OpenIG(string igName)
    {
        string url = "instagram.com/" + igName + "/";
        Debug.Log("Opening URL: " + url);
        Application.OpenURL(url);
    }
}
