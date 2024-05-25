using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class Musling : TouchCat
{
    private bool isPicked = false;

    protected override void Update()
    {
        if (isMoving)
        {
            MoveCat();
        } 

        // Check for touch input
        if (Touchscreen.current.primaryTouch.isInProgress)
        {
            TouchStart();
        }
        else
        {
            TouchEnd();
        }

        base.Update();

        // If the object is picked up, update its position to follow the touch
        if (isPicked)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            touchWorldPosition.z = 0; // Ensure the Z position is zero to avoid moving in depth
            transform.position = touchWorldPosition;
        }
    }
   
    protected override void TouchStart()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            
        // Check if touch starts over pusling
        if (!isTouching && catCollider.OverlapPoint(touchWorldPosition))
        {
            isTouching = true;
            PickUp();
            Debug.Log("Musling is picked up!");
        }
    }

    protected override void TouchEnd()
    {
        // Check if touch ends
        if (isTouching)
        {
            isTouching = false;
            PutDown();
            Debug.Log("Musling is put back down!");
        }
    }

    void PickUp()
    {
        SoundManager.Instance.PlayMusPick();
        isPicked = true;
        routine = false;
        isMoving = false;
        animator.SetBool("IsPicked", isPicked);
        UpdateAnimatorParameters();

    }

    void PutDown()
    {
        isPicked = false;
        animator.SetBool("IsPicked", isPicked);
        routine = true;

    }
}



