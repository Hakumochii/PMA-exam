using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class Musling : TouchCat
{
    private bool isPicked = false;

    protected override void Update()
    {
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
        isPicked = true;
        routine = false;
        isMoving = false;
        animator.SetBool("IsPicked", isPicked);
        UpdateAnimatorParameters();
        SoundManager.Instance.PlayMusPick();

    }

    protected override void TouchEnd()
    {
       
        isPicked = false;
        animator.SetBool("IsPicked", isPicked);
        routine = true;
    }
}



