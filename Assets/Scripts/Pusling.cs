using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pusling : Cat
{
    private bool isTouching = false;
    private Collider2D puslingCollider;
    private bool isPet = false;
    private float petTime = 10f;
    private bool timerSet = false;

    void Awake()
    {
        puslingCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveCat();
        } 
        
        // Check for touch input
        if (Touchscreen.current.primaryTouch.isInProgress)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            
            // Check if touch starts over Pusling
            if (!isTouching && puslingCollider.OverlapPoint(touchWorldPosition))
            {
                isTouching = true;
                LayDown();
                Debug.Log("Pusling is being pet!");
            }
        }
        else
        {
            // Check if touch ends
            if (isTouching)
            {
                isTouching = false;
                Debug.Log("Timer is set!");
                timerSet = true;
                StartCoroutine(GetUp());
            }
        }
    }

    void LayDown()
    {
        direction = Vector2.zero;
        isPet = true;
        isMoving = false;
        animator.SetBool("IsPet", isPet);
        UpdateAnimatorParameters();
        StopCoroutine(MoveCatRoutine());
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(petTime);
        Debug.Log("Pusling is standing!");
        timerSet = false;
        isPet = false;
        animator.SetBool("IsPet", isPet);
        StartCoroutine(MoveCatRoutine());
    }
}
