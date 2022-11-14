using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.L))
        {
            Application.OpenURL("https://forms.gle/Vr28Auvho8pBn9Fw5");
        }
    }
}
