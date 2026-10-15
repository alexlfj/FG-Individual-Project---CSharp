using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public int maxHealth;
    public float speed;
    public int attackDamage;
    public float xpValue;
}
