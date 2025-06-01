using UnityEngine;

public class ArcherGladiatorHealthHandler : IHealth
{
    private IEnemy enemy;
    private EnemySO enemySO;
    private Animator animator;
    private EnemyStateMachine stateMachine;
    private int maxHealth;
    private int health;

    public ArcherGladiatorHealthHandler(IEnemy enemy,EnemySO enemySO, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemySO = enemySO;
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        maxHealth = enemySO.maxHealth;
        health = maxHealth;

    }
    public float GetCurrentHealth()
    {
        if (health < 0)
            return 0;
        else
            return health;
    }

    public void TakeDamage(int damage)
    {
        if (enemy.IsDead) return;

        health -= damage;
        if (health <= 0)
        {
            stateMachine.ChangeEnemyState(new ArcherGladiatorDeathState(enemy, animator));
            enemy.IsDead = true;
        }
    }
}
