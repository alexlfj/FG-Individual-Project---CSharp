using UnityEngine;

public class playerXP : MonoBehaviour
{
    public static playerXP instance;

    public float experience;

    public float levelUpExperience;

    [SerializeField][Range (1f, 10f)] float levelingUpScale = 1.25f;

    [SerializeField] private AudioClip[] playerLevelUp;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        experience = 0;
        levelUpExperience = 100;
    }


    void Update()
    {
        
        if (experience >= levelUpExperience)
        {
            LevelUp();
        }
    }


    public void LevelUp()
    {
        PlayOnLevelUp();
        experience -= levelUpExperience;
        levelUpExperience = Mathf.RoundToInt(levelUpExperience * levelingUpScale);
        upgradeWindow.instance.Open();
    }

    private void PlayOnLevelUp()
    {

        if (SoundFXManager.Instance == null)
        {
            Debug.LogWarning("SoundFXManager not found in scene.");
            return;
        }
        else
        {
            SoundFXManager.Instance.PlayRandomAudioClips(playerLevelUp, transform, .5f);
        }
    }

}
