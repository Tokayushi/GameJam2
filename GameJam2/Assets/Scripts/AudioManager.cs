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
    [Range(0.0001f, 1f)] public float musicVolume = 0.5f;
    [Range(0.0001f, 1f)] public float sfxVolume = 0.5f;
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
       
    }

    private void Update()
    {
        MusicVolumeControl(musicVolume);
        SFXVolumeControl(sfxVolume);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip, sfxVolume);
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

    void MusicVolumeControl(float volume)
    {
        Master.SetFloat("Musica",Mathf.Log10(volume) * 20);


    }
    void SFXVolumeControl(float volume)
    {
        Master.SetFloat("SFX",Mathf.Log10(volume) * 20);
    }

}
