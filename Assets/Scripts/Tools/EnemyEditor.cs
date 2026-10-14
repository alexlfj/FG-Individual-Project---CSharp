using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeEditor : EditorWindow
{
    const string ResourcesPath = "Assets/Resources/Items";

    TextField nameField;
    TextField descriptionField;
    IntegerField maxHpField;
    Label statusLabel;

    [MenuItem("Tools/Enemy Creator")]
    static void Open()
    {
        var window = GetWindow<UpgradeEditor>();
        window.titleContent = new GUIContent("Enemy Creator");
        window.minSize = new Vector2(320, 220);
    }

    void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;

        root.Add(new Label("Create a new Enemy asset")
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 6 }
        });

        nameField = new TextField("Item Name");
        descriptionField = new TextField("Description") { multiline = true };
        descriptionField.style.minHeight = 60;
        maxHpField = new IntegerField("MaxHp");

        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(descriptionField);
        root.Add(maxHpField);
        root.Add(createButton);
        root.Add(statusLabel);
    }

    void CreateAsset()
    {
        if (string.IsNullOrWhiteSpace(nameField.value))
        {
            statusLabel.text = "Item name is required.";
            return;
        }

        Directory.CreateDirectory(ResourcesPath);

        //var enemy = ScriptableObject.CreateInstance<SO_Enemy>();
        //enemy.EnemyName = nameField.value;
        //enemy.Description = descriptionField.value;
        //enemy.MaxHP = maxHpField.value;

        //string safeName = string.Join("_", nameField.value.Split(Path.GetInvalidFileNameChars()));
        //string path = AssetDatabase.GenerateUniqueAssetPath($"{ResourcesPath}/{safeName}.asset");

        //AssetDatabase.CreateAsset(enemy, path);
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();

        //EditorGUIUtility.PingObject(enemy);
        //Selection.activeObject = enemy;
        //statusLabel.text = $"Created {path}";
    }
}