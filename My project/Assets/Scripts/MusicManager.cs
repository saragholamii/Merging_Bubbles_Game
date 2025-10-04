using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    
    public AudioSource audioSource;
    public AudioSource mergeSound;
    
    void Awake()
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
    
    void Start()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        
    }

    public void PlayMergeSound()
    {
        if (mergeSound != null)
            mergeSound.Play();
    }
}
