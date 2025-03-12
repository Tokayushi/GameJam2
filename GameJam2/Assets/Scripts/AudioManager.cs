using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    


    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource sfxAudio, musicAudio;
    public AudioClip initialMusic;
    [SerializeField] AudioMixer Master;
    [Header("Efectos de sonido")]
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip deathSound;
    [SerializeField] public AudioClip victorySound;
    [SerializeField] public AudioClip timeChangeSound;
   
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
    void Start()
    {
        sfxAudio = transform.GetChild(0).GetComponent<AudioSource>();
        musicAudio = transform.GetChild(1).GetComponent<AudioSource>();
        //iniciar la musica
        InitialPlayMusic(initialMusic);
    }

    
    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicAudio.Stop();
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;

    }
    public void InitialPlayMusic(AudioClip clip)
    {

        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }

    public void MusicVolumeControl(float volume)
    {
        Master.SetFloat("Musica",volume);


    }
    public void SFXVolumeControl(float volume)
    {
        Master.SetFloat("SFX",volume);
    }

}
