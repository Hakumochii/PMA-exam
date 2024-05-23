using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private float tiltThreshold = 0.1f; // Tilt threshold should be a small value
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Moves game object by tilting device
        if (Mathf.Abs(Input.acceleration.x) > tiltThreshold || Mathf.Abs(Input.acceleration.y) > tiltThreshold)
        {
            movement = new Vector2(Input.acceleration.x, Input.acceleration.y) * moveSpeed;
        }
        else
        {
            movement = Vector2.zero; // Stop movement when tilt is below the threshold
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement;
    }

}
