using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    internal Animator animator;
    internal Rigidbody2D rb;
    internal Vector2 direction;

    private float moveSpeed = 0.5f;
    private float moveTimeMin = 10f;  
    private float moveTimeMax = 20f; 
    internal float nextMoveWait = 2f;

    internal bool isMoving = false;
    internal bool routine = true;

    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    
    
    void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Start cat partolling
        StartCoroutine(MoveCatRoutine());
    }

    internal IEnumerator MoveCatRoutine()
    {
        while (routine)
        {
            // Get a direction to move in
            SetNewTargetDirection();

            // Move for a random duration within the range
            float moveDuration = Random.Range(moveTimeMin, moveTimeMax);
            yield return new WaitForSeconds(moveDuration);

            // Stop moving
            isMoving = false;
            direction = Vector2.zero;
            UpdateAnimatorParameters();
            yield return new WaitForSeconds(nextMoveWait); 
        }
    }

    internal void SetNewTargetDirection()
    {
        
        // Randomly choose a direction
        int randomIndex = Random.Range(0, directions.Length);
        direction = directions[randomIndex];

        // Update animator when starting to move
        isMoving = true;
        UpdateAnimatorParameters(); 
    }

    internal void Update()
    {
        if (isMoving)
        {
            MoveCat();
        }

    }

    internal void MoveCat()
    {
        // Move the cat in the current direction
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    internal void UpdateAnimatorParameters()
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetBool("IsMoving", isMoving);
        
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("wall"))
        {
            // Stop moving when colliding with walls
            isMoving = false;
            direction = Vector2.zero;
            UpdateAnimatorParameters();
        }

    }

}
