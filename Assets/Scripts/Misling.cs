using System.Collections;
using UnityEngine;

public class Misling : Cat
{
    private Transform fishTransform;
    private GameObject fishGameObject;

    private float chaseSpeed = 3f;
    private float reachedFishPosOffset = 5f;
    private float playingFishDis = 5f;

    private bool fishIsMoving;
    private float distanceToFish;
    private Vector3 directionToFish;

    private enum MislingState
    {
        Patrolling,
        ChasingFish,
        ReachedFish
    }

    private MislingState mislingState = MislingState.Patrolling;


    private void FindFish()
    {
        fishGameObject = GameObject.FindWithTag("Fish");
        fishTransform = fishGameObject.transform;
    }

    private void Update()
    {
        // If fish dissapears find it again
        if (fishGameObject == null)
        {
            FindFish();
        }

        // Define variable we use in multiple other states
        fishIsMoving = FishButton.Instance.FishMoving;
        distanceToFish = Vector2.Distance(rb.position, fishTransform.position);

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

    private void Patrol()
    {
        if (fishIsMoving)
        {
            SoundManager.Instance.PlayMisPlay();
            mislingState = MislingState.ChasingFish;
        }
        else
        {
            SoundManager.Instance.StopMisPlay();
            base.Update();
        }
    }

    private void ChaseFish()
    {
        if (!fishIsMoving)
        {
            StartCoroutine(MoveCatRoutine());
            mislingState = MislingState.Patrolling;
        }
        else
        {
            // Find direction of fish based on Misling then move torwards that direction and have the animator react accordingly
            isMoving = true;
            directionToFish = (fishTransform.position - (Vector3)rb.position).normalized;
            rb.MovePosition(rb.position + (Vector2)directionToFish * chaseSpeed * Time.deltaTime);
            UpdateAnimatorParameters(directionToFish);

            // if the distance of the fish is less than a set length/ if Misling is close enough to fish change state to reached fish
            if (distanceToFish < reachedFishPosOffset)
            {
                mislingState = MislingState.ReachedFish;
            }
        }
    }

    private void ReachedFish()
    {
        if (!fishIsMoving)
        {
            StartCoroutine(MoveCatRoutine());
            animator.SetBool("IsPlaying", false);
            mislingState = MislingState.Patrolling;
        }
        else
        {
            // stop moving and play "playing" animation
            isMoving = false;
            direction = Vector2.zero;
            UpdateAnimatorParameters();
            animator.SetBool("IsPlaying", true);

            // If fish moves a certain distance away form Misling start moving again, stop playing animation and chnge to chasing state
            if (distanceToFish > playingFishDis)
            {
                isMoving = true;
                animator.SetBool("IsPlaying", false);
                mislingState = MislingState.ChasingFish;
            }
        }  
    }

    // Overload "UpdateAnimatorParameters" to be able to take a perameter so that Mislings animations will act according to its direction while chasing fish 
    // without interferring with the current direction variable in Cat class
    private void UpdateAnimatorParameters(Vector3 movementDirection)
    {
        animator.SetFloat("MoveX", movementDirection.x);
        animator.SetFloat("MoveY", movementDirection.y);
        animator.SetBool("IsMoving", isMoving);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // While fish is moving Misling wont stop when bumping into walls
        if (!fishIsMoving && collision.gameObject.CompareTag("wall"))
        {
            isMoving = false;
            direction = Vector2.zero;
            UpdateAnimatorParameters();
        }
    }

}

