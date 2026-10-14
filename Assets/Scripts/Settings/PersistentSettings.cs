using UnityEngine;
using System.Collections;

public class PersistentSettings 
{
    const int DefaultHighScore = 0;
    const string s_HighScore = "HighScore";
    public static int HighScore
    {
        get 
        { 
            return PlayerPrefs.GetInt(s_HighScore, DefaultHighScore); 
        }

        set 
        { 
            PlayerPrefs.SetInt(s_HighScore, value);
            PlayerPrefs.Save();
        }
    }
}
