using UnityEngine;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{

    private playerXP playerExp;
    private playerHealth health;

    public UIDocument UIDoc;

    private ProgressBar xpBar;

    private Image[] hearts;


    void Start()
    {
        playerExp = playerXP.instance;
        health = playerHealth.instance;

        xpBar = UIDoc.rootVisualElement.Q<ProgressBar>("xpBar");


        // Hearts (total of 5 hearts)
        hearts = new Image[5];

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = UIDoc.rootVisualElement.Q<Image>("heart" + i);
        }

    }

    void Update()
    {
        
        // XP
        xpBar.value = playerExp.experience;
        xpBar.highValue = playerExp.levelUpExperience;
        xpBar.title = $"{playerExp.experience}/{playerExp.levelUpExperience}";


        // Health
        for (int i = 0; i < hearts.Length; i++)
        {
            int leftover = health.CurrentHealth - 2 * i;

            if (leftover >= 2)
            {
                hearts[i].sprite = health.fullHeart;
            }
            else if (leftover == 1)
            {
                hearts[i].sprite = health.halfHeart;
            }
            else
            {
                hearts[i].sprite = health.emptyHeart;
            }
        }
    }


}
