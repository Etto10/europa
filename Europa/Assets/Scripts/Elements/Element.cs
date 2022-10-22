using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public ElementType elementType;


    private void Start()
    {
        int x = Random.Range(1, 5);
        Vector3 rot = new(0, 0, x * 90f);
        transform.localRotation = Quaternion.Euler(rot);
    }
}
