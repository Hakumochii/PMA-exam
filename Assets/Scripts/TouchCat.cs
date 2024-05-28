using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;


public class TouchCat : Cat
{
    internal Collider2D catCollider;
    internal bool isTouching = false;


    protected virtual void Awake()
    {
        catCollider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        // Check for touch input
        if (Touchscreen.current.primaryTouch.isInProgress)
        {
            // Find the possition of the touch input
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                
            // Check if touch starts over cat
            if (!isTouching && catCollider.OverlapPoint(touchWorldPosition))
            {
                isTouching = true;
                TouchStart();
            }
        }
        else
        {
            // If there is no touch input but touch bool is true set it to false
            // Then do something (do something after a touch is done)
            if (isTouching)
            {
                isTouching = false;
                TouchEnd();
            }
            
        }

        base.Update();
    }
    
    // Placehoder methods to be overridden and used by child classes
    protected virtual void TouchStart(){}

    protected virtual void TouchEnd(){}
}