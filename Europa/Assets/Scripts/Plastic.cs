using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public GameObject playerCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OxygenGenerator"))
        {
            playerCollision.SetActive(true);
            Debug.Log("Plastic activated");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("OxygenGenerator"))
        {
            playerCollision.SetActive(true);
        }
    }
}
