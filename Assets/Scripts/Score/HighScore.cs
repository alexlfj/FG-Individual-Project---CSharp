using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private TMP_Text highScoreText;

    void Start()
    {
        highScoreText = GetComponent<TMP_Text>();
        highScoreText.text = "Current highscore: " + PersistentSettings.HighScore;
    }

}
