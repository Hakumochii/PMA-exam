using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;


public class TouchCat : Cat
{
    internal bool isTouching = false;
    internal Collider2D catCollider;

    protected virtual void Awake()
    {
        catCollider = GetComponent<Collider2D>();
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
            TouchStart();
        }
        else
        {
            TouchEnd();
        }
    }

    protected virtual void TouchStart()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            
        // Check if touch starts over cat
        if (!isTouching && catCollider.OverlapPoint(touchWorldPosition))
        {
            isTouching = true;
        }
    }

    protected virtual void TouchEnd()
    {
        // Check if touch ends
        if (isTouching)
        {
            isTouching = false;
        }
    }
}