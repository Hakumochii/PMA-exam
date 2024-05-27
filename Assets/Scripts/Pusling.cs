using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pusling : TouchCat
{
    private bool isPet = false;
    private float petTime = 10f;
    private CancellationTokenSource getUpCancellationTokenSource; // Token source to cancel the coroutine

    protected override void TouchStart()
    {
        SoundManager.Instance.PlayPusPet();
        direction = Vector2.zero;
        routine = false;
        isPet = true;
        isMoving = false;
        animator.SetBool("IsPet", isPet);
        UpdateAnimatorParameters();

        // Cancel any ongoing GetUp coroutine when laying down
        CancelGetUpCoroutine();
    }

    protected override void TouchEnd()
    {
        StartGetUpCoroutine();
    }

    private void StartGetUpCoroutine()
    {
        // Cancel any ongoing GetUp coroutine before starting a new one
        CancelGetUpCoroutine();

        getUpCancellationTokenSource = new CancellationTokenSource();
        StartCoroutine(GetUp(getUpCancellationTokenSource.Token));
    }

    private void CancelGetUpCoroutine()
    {
        if (getUpCancellationTokenSource != null)
        {
            getUpCancellationTokenSource.Cancel();
            getUpCancellationTokenSource = null;
        }
    }

    private IEnumerator GetUp(CancellationToken cancellationToken)
    {
        SoundManager.Instance.StopPusPet();
        float elapsedTime = 0f; // Track the elapsed time
        while (elapsedTime < petTime)
        {
            yield return null; // Wait for one frame
            elapsedTime += Time.deltaTime;

            // Check for cancellation
            if (cancellationToken.IsCancellationRequested)
                yield break;
        }

        // Only stand up if the full pet time has elapsed
        Debug.Log("Pusling is standing!");
        isPet = false;
        animator.SetBool("IsPet", isPet);
        routine = true;
        yield return new WaitForSeconds(nextMoveWait);
        isMoving = true;
        UpdateAnimatorParameters();

        // Ensure the token source is cleaned up
        getUpCancellationTokenSource = null;
    }
}
