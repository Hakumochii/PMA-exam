using UnityEngine;
using UnityEngine.InputSystem;

public class FishButton : MonoBehaviour
{
    private Collider2D buttonCollider;
    private GameObject currentFish;

    private Vector3 fishPosition = new Vector3(-2.91f, -6.62f, 0.0337346f);
    private Quaternion fishRotation = Quaternion.Euler(0f, 0f, -15.662f);

    [SerializeField] private GameObject fishPrefab; // Reference to the fish prefab

    private static FishButton _instance;
    public static FishButton Instance { get { return _instance; } }
    
    public bool FishMoving { get; private set; } = false; // Internal property to manage fish movement state


    private void Awake()
    {
        // Ensure there is only one instance of the FishButton class
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Define variables
        buttonCollider = GetComponent<Collider2D>();
        currentFish = GameObject.FindWithTag("Fish");
    }

    private void Update()
    {
        // Check for touch input
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            // Check if touch starts over the fish button
            if (buttonCollider.OverlapPoint(touchWorldPosition))
            {
                ToggleFishMovement();
            }

            // If fish has stopped moving destroy and make a new one (reset fish)
            if (!FishMoving)
            {
                if (currentFish != null)
                {
                    Destroy(currentFish);
                    currentFish = Instantiate(fishPrefab, fishPosition, fishRotation);
                }
            }
    
        }
 
    }

    internal void ToggleFishMovement()
    {
        // Change fish movement
        FishMoving = !FishMoving;
    }

}
