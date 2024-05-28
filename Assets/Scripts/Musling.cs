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

        // If Musling is picked up, update its position to follow the touch
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
        // Stop all current animation and movement an start new animation and sound
        SoundManager.Instance.PlayMusPick();
        isPicked = true;
        routine = false;
        isMoving = false;
        animator.SetBool("IsPicked", isPicked);
        UpdateAnimatorParameters();
    }

    protected override void TouchEnd()
    {
        // Continue the normal routine
        isPicked = false;
        animator.SetBool("IsPicked", isPicked);
        routine = true;
    }
}



