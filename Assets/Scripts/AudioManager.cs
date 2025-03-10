using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Références pour l'AudioSource et les clips
    private AudioSource audioSource;
    public AudioClip backgroundMusic; // Musique de fond pour GameScene
    public AudioClip buttonClickSound; // Son des boutons
    public AudioClip[] attackSounds; // Sons aléatoires pour les attaques de cartes

    private static AudioManager instance; // Singleton pour l'AudioManager

    void Awake()
    {
        // Si une instance existe déjà, détruire le nouvel objet
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Sinon, assigner cette instance à l'objet statique et empêcher sa destruction entre les scènes
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Récupérer le composant AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Jouer la musique de fond uniquement sur la scène "GameScene"
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.volume = 0.2f;
            audioSource.loop = true; // La musique de fond doit être en boucle
            audioSource.Play();
        }
    }

    // Arrêter la musique de fond
    public void StopBackgroundMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Jouer le son d'un bouton
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {

            audioSource.PlayOneShot(buttonClickSound); // On joue le son sans affecter la musique
        }
    }


    // Accéder à l'instance du AudioManager depuis n'importe quel script
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
