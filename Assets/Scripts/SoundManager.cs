using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        // Ensure there is always an instance of the sound manager
        get
        {
            // Check if the instance is null or has been destroyed
            if (instance == null || instance.gameObject == null)
            {
                // Find an existing instance in the scene
                instance = FindObjectOfType<SoundManager>();

                // If no instance exists, create a new one
                if (instance == null)
                {
                    GameObject obj = new GameObject(nameof(SoundManager));
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource pusPet;
    [SerializeField] private AudioSource musPick;
    [SerializeField] private AudioSource misPlay;

    private void Awake()
    {
        // Ensure the instance isn't destroyed when loading new scenes
        if (instance == null || instance.gameObject == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If another instance exists, destroy this one
            Destroy(gameObject);
            return;
        }
    }

    // Methods for playing audioclips 
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void PlayPusPet()
    {
        if (pusPet != null)
        {
            pusPet.Play();
        }
    }

    public void StopPusPet()
    {
        if (pusPet != null)
        {
            pusPet.Stop();
        }
    }

    public void PlayMusPick()
    {
        if (musPick != null)
        {
            musPick.Play();
        }
    }

    public void PlayMisPlay()
    {
        if (misPlay != null)
        {
            misPlay.Play();
        }
    }

    public void StopMisPlay()
    {
        if (misPlay != null)
        {
            misPlay.Stop();
        }
    }
}
