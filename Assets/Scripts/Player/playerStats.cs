using UnityEngine;

public class playerStats : MonoBehaviour
{

    [Header("Baseline Player Stats")]
    public int damage = 10;

    public float moveSpeed = 5f;

    public float attackSpeed = 360f;

    public int projectileCount = 1;
    public int maxProjectileCount = 5;

    public float pickupDistance = 2f;

    public float areaOfEffect = 0f;
    public float maxAreaOfEffect = 2f;

    public float recovery = 0f;

    public float growth = 0f;


    public void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.stat)
        {
            case StatType.Damage:
                damage += (int)upgrade.amount;
                break;

            case StatType.MoveSpeed:
                moveSpeed += upgrade.amount;
                break;

            case StatType.AttackSpeed:
                attackSpeed += upgrade.amount;
                break;

            case StatType.ProjectileCount:
                projectileCount = Mathf.Min(projectileCount + (int)upgrade.amount, maxProjectileCount);
                break;

            case StatType.PickupDistance:
                pickupDistance += upgrade.amount;
                break;

            case StatType.AreaOfEffect:
                areaOfEffect = Mathf.Min(areaOfEffect + upgrade.amount, maxAreaOfEffect);
                break;

            case StatType.Recovery:
                recovery += upgrade.amount;
                break;

            case StatType.Growth:
                growth += upgrade.amount;
                break;
        }
    }

    public float GetStat(StatType stat)
    {
        switch (stat)
        {
            case StatType.Damage:
                return damage;

            case StatType.MoveSpeed:
                return moveSpeed;

            case StatType.AttackSpeed:
                return attackSpeed;

            case StatType.ProjectileCount:
                return projectileCount;

            case StatType.PickupDistance:
                return pickupDistance;

            case StatType.AreaOfEffect:
                return areaOfEffect;

            case StatType.Recovery:
                return recovery;
                
            case StatType.Growth:
                return growth;
        }

        return 0f;
    }          
}
