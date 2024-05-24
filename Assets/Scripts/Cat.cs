using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direction;

    private float moveSpeed = 1.0f;
    private float decisionTimeMin = 10f;  
    private float decisionTimeMax = 20f; 

    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Start the movement coroutine
        StartCoroutine(MoveCatRoutine());
    }

    IEnumerator MoveCatRoutine()
    {
        while (true)
        {
            SetNewTargetDirection();

            // Move for a random duration within the range
            float moveDuration = Random.Range(decisionTimeMin, decisionTimeMax);
            yield return new WaitForSeconds(moveDuration);

            // Stop moving
            direction = Vector2.zero;
            isMoving = false;
            UpdateAnimatorParameters(); // Update animator when stopping
            yield return new WaitForSeconds(1f); // Pause for a moment before next move
        }
    }

    void SetNewTargetDirection()
    {
        // Randomly choose a direction: 0 = up, 1 = down, 2 = left, 3 = right
        int randomDirection = Random.Range(0, 4);

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
        }

        isMoving = true;
        UpdateAnimatorParameters(); // Update animator when starting to move
    }

    void Update()
    {
        if (isMoving)
        {
            MoveCat();
        }
    }

    void MoveCat()
    {
        // Move the cat in the current direction
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            // Stop all movement and reset direction
            direction = Vector2.zero;
            isMoving = false;
            UpdateAnimatorParameters(); // Update animator immediately on collision
            
            // Stop the current movement coroutine and start a new direction change routine
            StopCoroutine(MoveCatRoutine());
            StartCoroutine(WaitAndChangeDirection());
        }
    }

    IEnumerator WaitAndChangeDirection()
    {
        yield return new WaitForSeconds(0.1f); // Small delay before changing direction
        SetNewTargetDirection();
        StartCoroutine(MoveCatRoutine()); // Restart the movement coroutine
    }

    void UpdateAnimatorParameters()
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetBool("IsMoving", isMoving);
    }
}
