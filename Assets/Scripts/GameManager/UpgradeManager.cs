using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class upgradeWindow : MonoBehaviour
{
    public static upgradeWindow instance;

    public bool isOpen = false;

    public UIDocument GameUIDoc;

    private VisualElement GameUI;

    public UIDocument UpgradeUIDoc;

    private VisualElement UpgradeUI;

    [SerializeField] private List<UpgradeData> allUpgrades;

    private playerStats stats;
    private List<UpgradeData> choices = new List<UpgradeData>();    

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
        GameUI = GameUIDoc.rootVisualElement.Q<VisualElement>("Container");
        UpgradeUI = UpgradeUIDoc.rootVisualElement.Q<VisualElement>("Container");
        UpgradeUI.style.display = DisplayStyle.None;

        stats = playerHealth.instance.GetComponent<playerStats>();

        Button upgrade0 = UpgradeUI.Q<Button>("Upgrade0");
        upgrade0.clicked += OnUpgrade0Click;

        Button upgrade1 = UpgradeUI.Q<Button>("Upgrade1");
        upgrade1.clicked += OnUpgrade1Click;
    }

    public void Open()
    {
        PickUpgrades();
        ShowChoices();
        isOpen = true;
        Time.timeScale = 0f;
        GameUI.style.display = DisplayStyle.None;
        UpgradeUI.style.display = DisplayStyle.Flex;
    }

    public void Close()
    {
        isOpen = false;
        Time.timeScale = 1f;
        GameUI.style.display = DisplayStyle.Flex;
        UpgradeUI.style.display = DisplayStyle.None;
    }

    void PickUpgrades()
    {
        List<UpgradeData> available = new List<UpgradeData>();

        foreach (UpgradeData upgrade in allUpgrades)
        {
            if (upgrade.stat == StatType.AreaOfEffect && stats.areaOfEffect >= stats.maxAreaOfEffect)
            {
                continue;
            }

            if (upgrade.stat == StatType.ProjectileCount && stats.projectileCount >= stats.maxProjectileCount)
            {
                continue;
            }

            available.Add(upgrade);
        }

        choices.Clear();

        for (int i = 0; i < 2; i++)
        {
            int index = Random.Range(0, available.Count);
            choices.Add(available[index]);
            available.RemoveAt(index);
        }
    }
    void ShowChoices()
    {
        for (int i = 0; i < choices.Count; i++)
        {
            Label description = UpgradeUI.Q<Label>($"Description{i}");
            description.text = choices[i].description;

            VisualElement icon = UpgradeUI.Q<VisualElement>($"Icon{i}");
            icon.style.backgroundImage = new StyleBackground(choices[i].icon);

            float currentValue = stats.GetStat(choices[i].stat);
            float newValue = currentValue + choices[i].amount;

            if (choices[i].stat == StatType.AreaOfEffect)
            {
                newValue = Mathf.Min(newValue, stats.maxAreaOfEffect);
            }

            Label comparison = UpgradeUI.Q<Label>($"Comparison{i}");
            comparison.text = $"{choices[i].stat}: {currentValue} → {newValue}";
        }
    }

    void OnUpgrade0Click()
    {
        ChooseUpgrade(0);
    }

    void OnUpgrade1Click()
    {
        ChooseUpgrade(1);
    }

    void ChooseUpgrade(int index)
    {
        stats.ApplyUpgrade(choices[index]);
        Close();
    }
}