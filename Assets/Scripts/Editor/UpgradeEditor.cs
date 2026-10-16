using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeEditor : EditorWindow
{
    const string UpgradePath = "Assets/Scriptable Objects/Upgrades";

    TextField nameField;
    ObjectField iconField;
    TextField descriptionField;
    EnumField statField;
    FloatField amountField;
    Label statusLabel;



    [MenuItem("Tools/Upgrade Creator")]
    static void Open()
    {
        var window = GetWindow<UpgradeEditor>();
        window.titleContent = new GUIContent("Upgrade Creator");
        window.minSize = new Vector2(320, 220);
    }

    void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;

        root.Add(new Label("Create a new Upgrade")
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 6 }
        });

        nameField = new TextField("Upgrade Name");
        iconField = new ObjectField("Icon");
        iconField.objectType = typeof(Sprite);
        descriptionField = new TextField("Description");
        statField = new EnumField("Stat", StatType.Damage);
        amountField = new FloatField("Stat Amount");



        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(iconField);
        root.Add(descriptionField);
        root.Add(statField);
        root.Add(amountField);

        root.Add(createButton);
        root.Add(statusLabel);
    }

    void CreateAsset()
    {
        if (string.IsNullOrWhiteSpace(nameField.value))
        {
            statusLabel.text = "Upgrade name is required.";
            return;
        }

        Directory.CreateDirectory(UpgradePath);

        var upgrade = ScriptableObject.CreateInstance<UpgradeData>();
        upgrade.icon = iconField.value as Sprite;
        upgrade.description = descriptionField.value;
        upgrade.stat = (StatType)statField.value;
        upgrade.amount = amountField.value;


        string safeName = string.Join("_", nameField.value.Split(Path.GetInvalidFileNameChars()));
        string path = AssetDatabase.GenerateUniqueAssetPath($"{UpgradePath}/{safeName}.asset");

        AssetDatabase.CreateAsset(upgrade, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(upgrade);
        Selection.activeObject = upgrade;
        statusLabel.text = $"Created {path}";

        nameField.value = "";
        iconField.value = null;
        descriptionField.value = "";
        statField.value = StatType.Damage;
        amountField.value = 0f;


    }
}