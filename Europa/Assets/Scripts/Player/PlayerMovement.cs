using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField]private float speed;

    [Header("References")]
    private Rigidbody2D rb;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public Vector2 input;
    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(input.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (input.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * input.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CreepyMusic"))
        {
            AudioManager.Instance.CreepyMusic();
        }
    }
}
