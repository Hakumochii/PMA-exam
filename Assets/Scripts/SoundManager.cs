using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    // Add more fields and properties as needed
    public AudioSource backgroundMusic;
    public AudioSource pusPet;
    public AudioSource musPick;


    // Ensure only one instance of SoundManager exists
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(SoundManager).Name;
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    // Ensure the instance isn't destroyed when loading new scenes
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Example methods for playing sound effects
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

    
}

