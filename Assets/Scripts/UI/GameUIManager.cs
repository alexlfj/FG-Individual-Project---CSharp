using UnityEngine;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{

    private playerXP playerExp;
    private playerHealth health;

    public UIDocument UIDoc;

    private ProgressBar m_xpBar;

    void Start()
    {
        playerExp = playerXP.instance;
        health = playerHealth.instance;

        m_xpBar = UIDoc.rootVisualElement.Q<ProgressBar>("xpBar");
    }

    void Update()
    {
        
        // XP
        m_xpBar.value = playerExp.experience;
        m_xpBar.highValue = playerExp.levelUpExperience;
        m_xpBar.title = $"{playerExp.experience}/{playerExp.levelUpExperience}";


        // Health

    }


}
