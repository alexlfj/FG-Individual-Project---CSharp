using UnityEngine;

public enum StatType
{
    Damage,
    MoveSpeed,
    AttackSpeed,
    ProjectileCount,
    PickupDistance
}

[CreateAssetMenu(menuName = "New Upgrade")]
public class UpgradeData : ScriptableObject
{
    public Sprite icon;
    public string description;
    public StatType stat;
    public float amount;
}