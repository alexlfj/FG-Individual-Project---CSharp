using UnityEngine;
using TMPro;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Enemy slime;

    public float xpScore;

    private float elapsedTime;

    public float scoreMultiplier = 10;

    private float finalScore;

    public static ScoreManager instance;

    [SerializeField] private TMP_Text scoreText;

    private bool isRunning = true;

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

    
    void Update()
    {

        elapsedTime = Time.deltaTime;
        AddToScore(elapsedTime);

    }

    public void AddToScore(float xpAmount)
    {
        if (isRunning)
        {
            finalScore += scoreMultiplier * xpAmount;
            scoreText.text = "Score:" + Mathf.RoundToInt(finalScore);
        }
    }

    public void SaveHighScore()
    {
        isRunning = false;

        int score = Mathf.RoundToInt(finalScore);

        if (score > PersistentSettings.HighScore)
        {
            PersistentSettings.HighScore = score;
        }
    }

    
}
