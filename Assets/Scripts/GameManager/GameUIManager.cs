using UnityEngine;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{

    private playerXP playerExp;
    private playerHealth health;

    public UIDocument UIDoc;

    private VisualElement xpFill;

    private Label xpText;

    private Image[] hearts;


    void Start()
    {
        playerExp = playerXP.instance;
        health = playerHealth.instance;

        xpFill = UIDoc.rootVisualElement.Q<VisualElement>("xpFill");
        xpText = UIDoc.rootVisualElement.Q<Label>("xpText");


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
        float fraction = (float)playerExp.experience / playerExp.levelUpExperience;
        xpFill.style.width = Length.Percent(Mathf.Clamp01(fraction) * 100f);
        xpText.text = $"{Mathf.FloorToInt(playerExp.experience)}/{Mathf.FloorToInt(playerExp.levelUpExperience)}";

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
