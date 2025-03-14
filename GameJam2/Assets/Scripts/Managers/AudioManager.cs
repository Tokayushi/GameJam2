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
    public AudioClip WalkSound;

    private bool isInPast = false; // Comienza en el presente

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
            return;
        }
    }

    private void Start()
    {
        sfxAudio = transform.GetChild(0).GetComponent<AudioSource>();
        musicAudio = transform.GetChild(1).GetComponent<AudioSource>();

        // Cargar volúmenes desde PlayerPrefs
        LoadVolumeSettings();

        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Asegurar que la música inicie correctamente en la escena actual
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "GameScene")
        {
            isInPast = false; // Reiniciar el estado de tiempo
            PlayMusic(presentMusic);
        }
    }

    private void Update()
    {
        // Cambio de tiempo con la tecla "T"
        if (SceneManager.GetActiveScene().name == "GameScene" && Input.GetKeyDown(KeyCode.T))
        {
            ToggleTime();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxAudio.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicAudio.isPlaying && musicAudio.clip == clip) return; // Evita reiniciar la misma música

        musicAudio.Stop(); // Detener cualquier música anterior
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

    public void PlayJumpSound() => PlaySFX(jumpSound);
    public void PlayDeathSound() => PlaySFX(deathSound);
    public void PlayVictorySound() => PlaySFX(victorySound);
    public void PlayWalkSound() => PlaySFX(WalkSound);

    public void MusicVolumeControl(float volume)
    {
        Master.SetFloat("Musica", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SFXVolumeControl(float volume)
    {
        Master.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolumeSettings()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        Master.SetFloat("Musica", Mathf.Log10(musicVol) * 20);
        Master.SetFloat("SFX", Mathf.Log10(sfxVol) * 20);
    }
}











