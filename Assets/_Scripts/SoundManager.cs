using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null; // Singleton instance

    public AudioSource musicSource;             // Reference to the AudioSource for background music
    public AudioSource sfxSource;               // Reference to the AudioSource for sound effects

    public AudioClip backgroundMusic;           // Background music clip

    // Enum to easily reference specific sound effects
    public enum SoundEffect
    {
        OpenDoor,
        CloseDoor,
        OpenTable,
        CloseTable,
        Walk,
        Run,
        LightsOn,
        LightsOff
    }

    public AudioClip[] soundEffects;            // Array of sound effects

    void Awake()
    {
        // Ensure that only one instance of the SoundManager exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Set this to not be destroyed when reloading the scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    public void PlaySoundEffect(SoundEffect effect)
    {
        AudioClip clip = soundEffects[(int)effect];
        sfxSource.PlayOneShot(clip);
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
