using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Enemy Fx")]
    public AudioClip attackSfx;

    [Header("Stats")]
    public int maxHealth;
    public float moveSpeed;
    public float attackSpeed;
    public int attackDamage;
    public float attackRange;
    public float attackCooldown;

    [Header("Behavior")]
    public float detectionRange;
}
