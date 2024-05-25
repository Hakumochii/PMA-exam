using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class Musling : TouchCat
{
    /*private bool isPicked = false;
   
    protected override void TouchStart()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            
        // Check if touch starts over pusling
        if (!isTouching && catCollider.OverlapPoint(touchWorldPosition))
        {
            isTouching = true;
            Debug.Log("Musling is picked up!");
        }
    }

    protected override void TouchEnd()
    {
        // Check if touch ends
        if (isTouching)
        {
            isTouching = false;
            Debug.Log("Musling is put back down!");
        }
    }*/
}
