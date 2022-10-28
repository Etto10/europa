using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]private float growthTime, currentTime;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite grownSprite;
    [SerializeField] private BoxCollider2D pickUpCollider;

    private void Start()
    {
        currentTime = growthTime;
    }

    private void Update()
    {
        if(currentTime > 1f)
            currentTime -= Time.deltaTime;
        else
        {
            // Growing finished
            spriteRenderer.sprite = grownSprite;
            pickUpCollider.enabled = true;
        }

    }
}
