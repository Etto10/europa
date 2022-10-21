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
        }
        else if (collision.CompareTag("Soil"))
        {
            Soil soil = collision.gameObject.GetComponent<Soil>();

            soil.canPlaceSeed = true;
            Debug.Log("can place seed on " + soil.transform.name + "!");
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
