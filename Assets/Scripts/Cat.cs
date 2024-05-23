using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direction;

    private float moveSpeed = 1.0f;
    private float decisionTimeMin = 2f;  
    private float decisionTimeMax = 5f; 

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
    }

    void Update()
    {
        if (isMoving)
        {
            MoveCat();
        }

        // Update the animator parameters to reflect movement direction
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetBool("IsMoving", isMoving);
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
            SetNewTargetDirection();
        }
    }
}
