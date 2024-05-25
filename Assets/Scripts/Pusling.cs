using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class Pusling : TouchCat
{
    private bool isPet = false;
    private float petTime = 10f;
    private bool timerSet = false;

    protected override void TouchStart()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            
        // Check if touch starts over pusling
        if (!isTouching && catCollider.OverlapPoint(touchWorldPosition))
        {
            isTouching = true;
            LayDown();
            Debug.Log("Pusling is being pet!");
        }
    }

    protected override void TouchEnd()
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

    void LayDown()
    {
        direction = Vector2.zero;
        routine = false;
        isPet = true;
        isMoving = false;
        animator.SetBool("IsPet", isPet);
        UpdateAnimatorParameters();
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(petTime);
        Debug.Log("Pusling is standing!");
        timerSet = false;
        isPet = false;
        animator.SetBool("IsPet", isPet);
        routine = true;
    }

}