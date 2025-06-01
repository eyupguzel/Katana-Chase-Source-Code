using UnityEngine;

public class HealthHandler : IHealth
{
    private IEnemy enemy;
    private EnemySO enemySO;
    private float maxHealth;
    private float health;

    private Animator animator;
    private EnemyStateMachine stateMachine;
    public HealthHandler(IEnemy enemy,EnemySO enemySO,Animator animator,EnemyStateMachine stateMachine)
    {
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.enemySO = enemySO;

        maxHealth = enemySO.maxHealth;
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if(enemy.IsDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
            enemy.IsDead = true;
        }
    }
    private void Die()
    {
        stateMachine.ChangeEnemyState(new KnightDeathState(enemy,animator));
    }
    public float GetCurrentHealth()
    {
        if (health < 0)
            return 0;
        else
            return health;
    }
}
