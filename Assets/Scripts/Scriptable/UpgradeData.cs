using UnityEngine;

public enum StatType
{
    Damage,
    MoveSpeed,
    AttackSpeed,
    ProjectileCount,
    PickupDistance,
    AreaOfEffect,
    Recovery,
    Growth
}

[CreateAssetMenu(menuName = "New Upgrade")]
public class UpgradeData : ScriptableObject
{
    public Sprite icon;
    public string description;
    public StatType stat;
    public float amount;
}