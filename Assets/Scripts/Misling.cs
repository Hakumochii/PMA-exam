using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misling : Cat
{
    private Transform fishTransform; // Reference to the fish's transform
    private float chaseSpeed = 3f; // Speed at which Misling chases the fish
    private GameObject fishGameObject; // Reference to the fish's GameObject
    private float reachedFishPosOffset = 5;
    private float playingFishDis = 7;
    private bool fishIsMoving;

    // Enum to represent Misling's states
    private enum MislingState
    {
        Patrolling,
        ChasingFish,
        ReachedFish
    }

    private MislingState mislingState = MislingState.Patrolling; // Initialize Misling's state to patrolling

    void Start()
    {
        // Get references to components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Start the movement coroutine
        StartCoroutine(MoveCatRoutine());

        // Find the fish GameObject by its tag "Fish"
        fishGameObject = GameObject.FindWithTag("Fish");

        // If the fish GameObject is found, get its transform component
        if (fishGameObject != null)
        {
            fishTransform = fishGameObject.transform;
        }
        else
        {
            Debug.LogError("Fish GameObject not found!");
        }
    }

    void Update()
    {
        fishIsMoving = FishButton.Instance.FishMoving;
       
        switch (mislingState)
        {
            case MislingState.Patrolling:
                Patrol();
                break;
            case MislingState.ChasingFish:
                ChaseFish();
                break;
            case MislingState.ReachedFish:
                ReachedFish();
                break;
        }
    }

    void Patrol()
    {
        if (fishIsMoving)
        {
            // If the fish is moving, start chasing it
            mislingState = MislingState.ChasingFish;
        }
        else
        {
            // If the fish is not moving, Misling roams randomly
            base.Update();
        }
        
    }

    void ChaseFish()
    {
        if (fishIsMoving)
        {
            if (fishTransform != null)
            {
                Vector3 rbPos3D = new Vector3(rb.position.x, rb.position.y, 0f); // Convert rb.position to Vector3
                Vector3 directionToFish = (fishTransform.position - rbPos3D).normalized;

                // Add an offset for the position of the fish that the cat is chasing
                Vector3 chasePosition = fishTransform.position - (directionToFish * reachedFishPosOffset);

                // Move towards the chase position
                rb.MovePosition(rb.position + (Vector2)(chasePosition - rbPos3D).normalized * chaseSpeed * Time.deltaTime);

                // Check if Misling has reached the fish
                float distanceToFish = Vector2.Distance(rb.position, fishTransform.position);
                if (distanceToFish < reachedFishPosOffset)
                {
                    // Change Misling's state to ReachedFish if it reaches the fish
                    mislingState = MislingState.ReachedFish;
                }

                // Update animator parameters based on movement direction
                UpdateAnimatorParameters(directionToFish);
            }
        }
        else
        {
            mislingState = MislingState.Patrolling;
        }
    }

    void ReachedFish()
    {
        if (fishIsMoving)
        {
            // Perform actions when Misling reaches the fish
            direction = Vector2.zero;
            isMoving = false;
            animator.SetBool("IsPlaying", true);
            UpdateAnimatorParameters();

            // Change Misling's state back to Patrolling after reaching the fish
            float distanceToFish = Vector2.Distance(rb.position, fishTransform.position);
            
            if (distanceToFish > playingFishDis)
            {
                isMoving = true;
                animator.SetBool("IsPlaying", false);
                UpdateAnimatorParameters();
                mislingState = MislingState.ChasingFish;
            }
        }
        else
        {
            animator.SetBool("IsPlaying", false);
            mislingState = MislingState.Patrolling;
        }
        
    }

    // Method to update animator parameters based on movement direction
    void UpdateAnimatorParameters(Vector3 movementDirection)
    {
        animator.SetFloat("MoveX", movementDirection.x);
        animator.SetFloat("MoveY", movementDirection.y);
        animator.SetBool("IsMoving", isMoving);
    }
}
