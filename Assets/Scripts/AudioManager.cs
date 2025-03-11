using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip backgroundMusic; 
    public AudioClip buttonClickSound;
    public AudioClip[] attackSounds;

    private static AudioManager instance; 

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.volume = 0.2f;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {

            audioSource.PlayOneShot(buttonClickSound);
        }
    }


    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("AudioManager instance not found.");
            }
            return instance;
        }
    }
}
