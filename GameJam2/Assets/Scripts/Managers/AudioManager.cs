using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource sfxAudio, musicAudio;
    [SerializeField] private AudioMixer Master;

    [Header("Música de Fondo")]
    public AudioClip mainMenuMusic;
    public AudioClip pastMusic;
    public AudioClip presentMusic;

    [Header("Efectos de sonido")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip victorySound;
    public AudioClip timeChangeSound;

    private bool isInPast = true; // Indica si el jugador está en el pasado

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sfxAudio = transform.GetChild(0).GetComponent<AudioSource>();
        musicAudio = transform.GetChild(1).GetComponent<AudioSource>();

        // Reproducir la música según la escena
        UpdateMusic(SceneManager.GetActiveScene().name);

        // Suscribirse al cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusic(scene.name);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && Input.GetKeyDown(KeyCode.T))
        {
            ToggleTime();
        }
    }

    private void UpdateMusic(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (sceneName == "GameScene")
        {
            PlayMusic(isInPast ? pastMusic : presentMusic);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicAudio.clip == clip) return; // Evita reiniciar la misma música

        musicAudio.Stop();
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }

    private void ToggleTime()
    {
        isInPast = !isInPast;
        PlayMusic(isInPast ? pastMusic : presentMusic);
        PlaySFX(timeChangeSound);
    }

    public void MusicVolumeControl(float volume)
    {
        Master.SetFloat("Musica", volume);
    }

    public void SFXVolumeControl(float volume)
    {
        Master.SetFloat("SFX", volume);
    }
}
