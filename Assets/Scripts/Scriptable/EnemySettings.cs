using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy")]
public class EnemySettings : ScriptableObject
{
    public int maxHealth;
    public float speed;
    public int attackDamage;
    public float xpValue;
}
