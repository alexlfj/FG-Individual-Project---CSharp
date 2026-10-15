using UnityEngine;

public class playerStats : MonoBehaviour
{
    public int damage = 10;
    public float moveSpeed = 5f;
    public float attackSpeed = 360f;
    public int projectileCount = 1;
    public float pickupDistance = 2f;


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
                projectileCount += (int)upgrade.amount;
                break;

            case StatType.PickupDistance:
                pickupDistance += upgrade.amount;
                break;
        }
    }

}
