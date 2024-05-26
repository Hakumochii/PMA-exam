using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private float tiltThreshold = 0.1f; // Tilt threshold should be a small value
    private Vector2 movement;
    private Vector2 initialPosition;
    public Vector2 InitialPosition
    {
        get { return initialPosition; }
        private set { initialPosition = value; }
    }
    public bool fishMoving = false;
    private float noGravity = 0;
    private float fishGravity = 1;
    private Collider2D fishCollider;
    private bool isTouched = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = noGravity;
        initialPosition = transform.position; // Use transform.position for initial position
        fishCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Check for touch input
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            // Check if touch starts over the fish
            if (fishCollider.OverlapPoint(touchWorldPosition))
            {
                ToggleFishMovement();
            }
        }

        if (fishMoving)
        {
            // Apply gravity and movement when the fish is moving
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
        else if (!isTouched)
        {
            // Turn off gravity and reset position when the fish is not moving
            rb.gravityScale = noGravity;
            transform.position = initialPosition; // Reset to initial position using transform.position
            rb.velocity = Vector2.zero;
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (fishMoving)
        {
            rb.velocity = movement;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void ToggleFishMovement()
    {
        fishMoving = !fishMoving;
        isTouched = true;

        if (!fishMoving)
        {
            // When stopping the fish, destroy the GameObject and instantiate a new one
            Instantiate.Instance.InsNewFish(initialPosition);
            Destroy(gameObject);
        }
    }
}
