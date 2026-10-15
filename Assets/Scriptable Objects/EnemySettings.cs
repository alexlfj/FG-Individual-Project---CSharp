using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public int attackDamage;
    public float xpValue;
}
