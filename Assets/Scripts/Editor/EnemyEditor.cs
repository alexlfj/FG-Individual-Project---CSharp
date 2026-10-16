using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyEditor : EditorWindow
{
    const string EnemyPath = "Assets/Scriptable Objects/Enemies";

    TextField nameField;
    EnumField behaviorField;
    IntegerField maxHealthField;
    FloatField speedField;
    IntegerField attackDamageField;
    FloatField xpValueField;
    Label statusLabel;
    FloatField waveSpeedField;
    FloatField waveWidthField;




    [MenuItem("Tools/Enemy Creator")]
    static void Open()
    {
        var window = GetWindow<EnemyEditor>();
        window.titleContent = new GUIContent("Enemy Creator");
        window.minSize = new Vector2(320, 220);
    }

    void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = root.style.paddingRight = 8;
        root.style.paddingTop = 8;

        root.Add(new Label("Create a new Enemy")
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 6 }
        });

        nameField = new TextField("Enemy Name");
        behaviorField = new EnumField("Behavior", EnemyBehaviorType.Chase);
        maxHealthField = new IntegerField("Max Health");
        speedField = new FloatField("Speed");
        attackDamageField = new IntegerField("Attack Damage");
        xpValueField = new FloatField("XP Value");
        waveSpeedField = new FloatField("Wave Speed (Zigzag)");
        waveWidthField = new FloatField("Wave Width (Zigzag)");


        var createButton = new Button(CreateAsset) { text = "Create" };
        createButton.style.marginTop = 8;

        statusLabel = new Label();
        statusLabel.style.marginTop = 6;
        statusLabel.style.whiteSpace = WhiteSpace.Normal;

        root.Add(nameField);
        root.Add(behaviorField);
        root.Add(maxHealthField);
        root.Add(speedField);
        root.Add(attackDamageField);
        root.Add(xpValueField);

        root.Add(waveSpeedField);
        root.Add(waveWidthField);

        root.Add(createButton);
        root.Add(statusLabel);
    }

    void CreateAsset()
    {
        if (string.IsNullOrWhiteSpace(nameField.value))
        {
            statusLabel.text = "Enemy name is required.";
            return;
        }

        Directory.CreateDirectory(EnemyPath);

        var enemy = ScriptableObject.CreateInstance<EnemySettings>();
        enemy.maxHealth = maxHealthField.value;
        enemy.behavior = (EnemyBehaviorType)behaviorField.value;
        enemy.speed = speedField.value;
        enemy.attackDamage = attackDamageField.value;
        enemy.xpValue = xpValueField.value;
        enemy.waveSpeed = waveSpeedField.value;
        enemy.waveWidth = waveWidthField.value;

        string safeName = string.Join("_", nameField.value.Split(Path.GetInvalidFileNameChars()));
        string path = AssetDatabase.GenerateUniqueAssetPath($"{EnemyPath}/{safeName}.asset");

        AssetDatabase.CreateAsset(enemy, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorGUIUtility.PingObject(enemy);
        Selection.activeObject = enemy;
        statusLabel.text = $"Created {path}";


        nameField.value = "";
        behaviorField.value = EnemyBehaviorType.Chase;
        maxHealthField.value = 0;
        speedField.value = 0f;
        attackDamageField.value = 0;
        xpValueField.value = 0f;
        waveSpeedField.value = 1f;
        waveWidthField.value = 1f;
    }
}