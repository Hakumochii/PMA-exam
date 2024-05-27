using UnityEngine;
using UnityEngine.InputSystem;

public class TouchCat : Cat
{
    internal Collider2D catCollider;
    protected Camera mainCamera;

    protected virtual void Awake()
    {
        catCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    protected virtual void Update()
    {
        HandleTouchInput();
        base.Update();
    }

    protected virtual void HandleTouchInput()
    {
        if (Touchscreen.current.primaryTouch.isInProgress)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            
            if (catCollider.OverlapPoint(touchWorldPosition))
            {
                TouchStart();
            }
        }
        else
        {
            TouchEnd();
        }
    }

    protected virtual void TouchStart(){}

    protected virtual void TouchEnd(){}
}
