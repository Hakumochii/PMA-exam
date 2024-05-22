using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direction;

    private float moveSpeed = 1.0f;
    private float decisionTimeMin = 5f;
    private float decisionTimeMax = 10f;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private Vector2 targetPosition;
    private float decisionTime;

    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        minBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
        maxBounds = Camera.main.ScreenToWorldPoint(new Vector2(1536, 2048)); // Top-right corner

        // Start moving the cat
        SetNewTargetDirection();
    }

    void SetNewTargetDirection()
    {
        // Randomly choose a direction: 0 = up, 1 = down, 2 = left, 3 = right, 4 = no movement
        int randomDirection = Random.Range(0, 5);

        switch (randomDirection)
        {
            case 0:
                direction = Vector2.up;
                break;
            case 1:
                direction = Vector2.down;
                break;
            case 2:
                direction = Vector2.left;
                break;
            case 3:
                direction = Vector2.right;
                break;
            case 4:
                direction = Vector2.zero;
                break;
        }

        // Calculate new target position based on the chosen direction
        targetPosition = (Vector2)transform.position + direction * moveSpeed;

        // Ensure the target position is within bounds
        ClampTargetPositionToBounds();

        // Set new decision time after the current movement is complete
        decisionTime = Random.Range(decisionTimeMin, decisionTimeMax);
        Invoke("SetNewTargetDirection", decisionTime);
    }

    void ClampTargetPositionToBounds()
    {
        // Clamp the target position to stay within bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
    }

    void Update()
    {
        MoveCat();

        // Update the animator parameters to reflect movement direction
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        // Check if the cat is not moving (direction is zero)
        if (!isMoving)
        {
            // Play the idle animation when the cat is not moving
            animator.SetBool("IsMoving", false);
        }
        else
        {
            // Play the walking animation when the cat is moving
            animator.SetBool("IsMoving", true);
        }
    }

    void MoveCat()
    {
        // Check if the cat has reached the target position
        if ((Vector2)transform.position != targetPosition)
        {
            // Move the cat towards the target position
            rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime));
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
