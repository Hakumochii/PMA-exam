using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    private static Instantiate _instance;
    public static Instantiate Instance { get { return _instance; } }
    public GameObject fishPrefab; // Reference to the fish prefab

    private void Awake()
    {
        // Ensure there is only one instance of the Instantiate class
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void InsNewFish(Vector2 initialPosition)
    {
        Instantiate(fishPrefab, initialPosition, Quaternion.identity);
    }
}
