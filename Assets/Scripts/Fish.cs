using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private float tiltThreshold = 0.1f; // Tilt threshold should be a small value
    private Vector2 movement;
    private Vector2 initialPosition;
    public bool fishMoving = false;
    private float noGravity = 0;
    private float fishGravity = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = noGravity;
        initialPosition = rb.position;
    }

    void Update()
    {
        if (fishMoving)
        {
            Debug.Log("Fish is now moving (Update)");
            rb.gravityScale = fishGravity;

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
        else
        {
            rb.gravityScale = noGravity;
            //return to original position
            movement = Vector2.zero;
            rb.position = initialPosition;
            
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement;
    }

    public void FishMove()
    {
       fishMoving = !fishMoving;
    }

}
