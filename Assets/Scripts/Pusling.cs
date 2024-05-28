using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pusling : TouchCat
{
    private bool isPet = false;
    private float petTime = 10f;
    private Coroutine getUpCoroutine;

    protected override void TouchStart()
    {
        // Stop patrol routine and stop moving then play petting animation and sound
        direction = Vector2.zero;
        isMoving = false;
        isPet = true;
        routine = false;
        UpdateAnimatorParameters();
        animator.SetBool("IsPet", isPet);
        SoundManager.Instance.PlayPusPet();

        if (getUpCoroutine != null)
        {
            StopCoroutine(getUpCoroutine);
            getUpCoroutine = null;
        }
    }

    protected override void TouchEnd()
    {
        // Stop playing purring sound ans start the GetUp coroutine when touch ends
        SoundManager.Instance.StopPusPet();
        getUpCoroutine = StartCoroutine(GetUp());
    }

    private IEnumerator GetUp()
    {
        yield return new WaitForSeconds(petTime);

        // Only stand up(stop playing petting and start routine again) if the full pet time has elapsed
        isPet = false;
        isMoving = true;
        routine = true;
        UpdateAnimatorParameters();
        animator.SetBool("IsPet", isPet);
        StartCoroutine(MoveCatRoutine());
    }
}
