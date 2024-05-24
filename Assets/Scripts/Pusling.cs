using UnityEngine;
using UnityEngine.InputSystem;

public class Pusling : Cat
{
    private InputAction petAction;
    private Collider2D puslingCollider;

    void Awake()
    {
        // Create a new action map
        var actionMap = new InputActionMap();

        // Add a new action to the action map
        petAction = actionMap.AddAction("Pet", binding: "<Touchscreen>/primaryTouch/position");

        // Enable the action map
        actionMap.Enable();

        // Subscribe to the action's performed event
        petAction.performed += OnPetPerformed;

        // Get reference to the collider attached to the Pusling
        puslingCollider = GetComponent<Collider2D>();
    }

    void OnDestroy()
    {
        // Disable the action map
        petAction?.actionMap?.Disable();

        // Unsubscribe from the action's performed event
        petAction.performed -= OnPetPerformed;
    }

    private void OnPetPerformed(InputAction.CallbackContext context)
    {
        // Get the touch position from the context
        Vector2 touchPosition = context.ReadValue<Vector2>();

        // Convert touch position to world space
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        // Check if the touch position overlaps with the Pusling's collider
        if (puslingCollider != null && puslingCollider.OverlapPoint(touchWorldPosition))
        {
            // Handle the touch interaction
            Debug.Log("Pusling was touched!");
            // You can add your own logic here, such as triggering an animation or sound
        }
    }
}
