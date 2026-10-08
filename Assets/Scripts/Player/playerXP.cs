using UnityEngine;

public class playerXP : MonoBehaviour
{
    public static playerXP instance;

    public float experience;

    public float levelUpExperience;

    [SerializeField][Range (1f, 10f)] float levelingUpScale = 1.25f;

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
        //experience ++; 

        if (experience >= levelUpExperience)
        {
            LevelUp();
        }
    }


    public void LevelUp()
    {
        experience = 0;
        levelUpExperience = Mathf.RoundToInt(levelUpExperience * levelingUpScale);
    }



}
