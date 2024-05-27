using UnityEngine.InputSystem;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private float tiltThreshold = 0.1f; // Tilt threshold should be a small value
    private Vector2 movement;
    private float noGravity = 0;
    private float fishGravity = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = noGravity;
    }

    private void Update()
    {
        if (FishButton.Instance.FishMoving)
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
    }

    private void FixedUpdate()
    {
        if (FishButton.Instance.FishMoving)
        {
            rb.velocity = movement;
        }
    }
}
