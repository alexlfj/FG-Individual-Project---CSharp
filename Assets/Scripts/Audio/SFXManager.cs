using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    
    [Header("Audio Source")]
    [SerializeField] private AudioSource SFXSource;

    
    public static SoundFXManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayRandomAudioClips(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {


        int rand = Random.Range(0, audioClip.Length);

        float pitchRand = Random.Range(0.95f, 1.05f);

        AudioSource audioSource = Instantiate(SFXSource, spawnTransform.position, Quaternion.identity);

        DontDestroyOnLoad(audioSource.gameObject);
        
        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.pitch = pitchRand;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayRandomAudioClipsNoRandPitch(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {


        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(SFXSource, spawnTransform.position, Quaternion.identity);

        DontDestroyOnLoad(audioSource.gameObject);

        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }
}