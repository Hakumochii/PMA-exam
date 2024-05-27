using UnityEngine.InputSystem;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;

    private float moveSpeed = 5f;
    private float tiltThreshold = 0.1f; 

    private float noGravity = 0;
    private float fishGravity = 1;

    private bool isFishMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = noGravity;
    }

    private void Update()
    {
        isFishMoving = FishButton.Instance.FishMoving;

        if (isFishMoving && (Mathf.Abs(Input.acceleration.x) > tiltThreshold || Mathf.Abs(Input.acceleration.y) > tiltThreshold))
        {
            // Apply gravity when the fish is moving
            rb.gravityScale = fishGravity;

            movement = new Vector2(Input.acceleration.x, Input.acceleration.y) * moveSpeed;
        }
        else
        {
            movement = Vector2.zero; // Stop movement when tilt is below the threshold
        }
    }

    private void FixedUpdate()
    {
        if (isFishMoving)
        {
            rb.velocity = movement;
        }
    }
}
