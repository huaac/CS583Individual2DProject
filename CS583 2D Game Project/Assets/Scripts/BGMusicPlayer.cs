using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip menuMusic;

    [SerializeField]
    /// <summary>
    /// The component that plays the music
    /// </summary>
    private AudioSource source;

    /// <summary>
    /// This class follows the singleton pattern and this is its instance
    /// </summary>
    static private BGMusicPlayer instance;

    /// <summary>
    /// Awake is not public because other scripts have no reason to call it directly,
    /// only the Unity runtime does (and it can call protected and private methods).
    /// It is protected virtual so that possible subclasses may perform more specific
    /// tasks in their own Awake and still call this base method (It's like constructors
    /// in object-oriented languages but compatible with Unity's component-based stuff.
    /// </summary>
    // protected virtual void Awake() 
    // {
    //     // Singleton enforcement
    //     if (instance == null) 
    //     {
    //         // Register as singleton if first
    //         instance = this;
    //         DontDestroyOnLoad(this);
    //     } 
    //     else {
    //         // Self-destruct if another instance exists
    //         Destroy(this);
    //         return;
    //     }
    //  }

    protected virtual void Awake() 
    {
        // Singleton enforcement
        if (instance != null && instance != this)
        {
            // Self-destruct if another instance exists
            Destroy(this);
            return;
        } 
        else {
            // Register as singleton if first
            instance = this;
            DontDestroyOnLoad(this);
        }
     }


    protected virtual void Start() 
    {
        // If the game starts in a menu scene, play the appropriate music
        PlayMenuMusic();
    }

    /// <summary>
    /// Plays the music designed for the menus
    /// This method is static so that it can be called from anywhere in the code.
    /// </summary>
    static public void PlayMenuMusic ()
    {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.menuMusic;
                instance.source.Play();
            }
        } 
        else 
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }


    // /// <summary>
    // /// Plays the music designed for the opening cutscene
    // /// This method is static so that it can be called from anywhere in the code.
    // /// </summary>
    // static public void PlayOpeningMusic()
    // {
    //     if (instance != null) {
    //         if (instance.source != null) {
    //             instance.source.Stop();
    //             instance.source.clip = instance.openingMusic;
    //             instance.source.Play();
    //         }
    //     } 
    //     else 
    //     {
    //         Debug.LogError("Unavailable MusicPlayer component");
    //     }
    // }
     
    // /// <summary>
    // /// Plays the music designed for boss fight
    // /// This method is static so that it can be called from anywhere in the code.
    // /// </summary>
    // static public void PlayBossMusic ()
    // {
    //     if (instance != null) {
    //         if (instance.source != null) {
    //             instance.source.Stop();
    //             instance.source.clip = instance.bossMusic;
    //             instance.source.Play();
    //         }
    //      } 
    //     else
    //     {
    //         Debug.LogError("Unavailable MusicPlayer component");
    //     }
    // }

    // /// <summary>
    // /// Plays the music designed for end music
    // /// This method is static so that it can be called from anywhere in the code.
    // /// </summary>
    // static public void PlayEndingMusic ()
    // {
    //     if (instance != null) {
    //         if (instance.source != null) {
    //             instance.source.Stop();
    //             instance.source.clip = instance.endingMusic;
    //             instance.source.Play();
    //         }
    //      } 
    //     else
    //     {
    //         Debug.LogError("Unavailable MusicPlayer component");
    //     }
    // }

}