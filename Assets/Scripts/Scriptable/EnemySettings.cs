using UnityEngine;

public enum EnemyBehaviorType
{
    Chase,
    Zigzag,
    Straightline
}



[CreateAssetMenu(menuName = "New Enemy")]
public class EnemySettings : ScriptableObject
{
    public EnemyBehaviorType behavior;
    public int maxHealth;
    public float speed;
    public int attackDamage;
    public float xpValue;


    [Header("Zigzag only")]
    public float waveSpeed = 1f;   
    public float waveWidth = 1f; 
}
